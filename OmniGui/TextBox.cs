namespace OmniGui
{
    using System;
    using System.Linq;
    using Zafiro.Core;
    using Zafiro.PropertySystem.Standard;

    public class TextBox : Layout
    {
        private Brush foreground;

        public static readonly ExtendedProperty FontSizeProperty = PropertyEngine.RegisterProperty("FontSize", typeof(TextBox),
            typeof(float), new PropertyMetadata { DefaultValue = 16F });

        public static readonly ExtendedProperty FontWeightProperty = PropertyEngine.RegisterProperty("FontWeight", typeof(TextBox),
            typeof(float), new PropertyMetadata { DefaultValue = FontWeights.Normal });

        public static readonly ExtendedProperty FontFamilyProperty = PropertyEngine.RegisterProperty("FontFamily", typeof(TextBox),
            typeof(float), new PropertyMetadata { DefaultValue = "Arial" });

        public static readonly ExtendedProperty TextProperty = PropertyEngine.RegisterProperty("Text", typeof(TextBox),
            typeof(string), new PropertyMetadata { DefaultValue = null });

        private readonly TextBlock textBlock = new TextBlock();

        public TextBox()
        {
            this.AddChild(new Border
            {
                BorderBrush = new Brush(Colors.Black),
                BorderThickness = 1,
                Padding = new Thickness(2),
                
            }.AddChild(textBlock));

            Pointer.Down.Subscribe(point => Platform.Current.SetFocusedElement(this));

            this.NotifyRenderAffectedBy(TextProperty);

            Foreground = new Brush(Colors.Black);
            GetChangedObservable(TextProperty).Subscribe(t => Text = (string) t);
            Keyboard.KeyInput.Subscribe(args => Text = ProcessKeyInput(args));
        }

        private string ProcessKeyInput(KeyInputArgs args)
        {
            if (args.Text.First() == Chars.Backspace)
            {
                return new string(Text.DropLast(1).ToArray());
            }

            return string.Concat(Text, args.Text);
        }

        private string ProcessTextInput(TextInputArgs args)
        {
            if (args.Text.First() == Chars.Backspace)
            {
                return new string(Text.DropLast(1).ToArray());
            }

            return string.Concat(Text, args.Text);
        }
      
        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set
            {
                SetValue(FontFamilyProperty, value);
            }
        }

        public FontWeights FontWeight
        {
            get { return (FontWeights)GetValue(FontWeightProperty); }
            set
            {
                SetValue(FontWeightProperty, value);
            }
        }

        public float FontSize
        {
            get { return (float)GetValue(FontSizeProperty); }
            set
            {
                SetValue(FontSizeProperty, value);
            }
        }

        public Brush Foreground
        {
            get { return foreground; }
            set
            {
                foreground = value;
            }
        }

        public string Text
        {
            get
            {
                return textBlock.Text;
            }
            set
            {
                SetValue(TextProperty, value);
                textBlock.Text = value;
            }
        }

        public TextWrapping TextWrapping { get; set; }
    }
}