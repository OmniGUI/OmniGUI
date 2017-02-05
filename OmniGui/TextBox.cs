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
            typeof(float), new PropertyMetadata { DefaultValue = FontWeight.Normal });

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

            Foreground = new Brush(Colors.Black);
            GetChangedObservable(TextProperty).Subscribe(t => Text = (string) t);
            Keyboard.TextInput.Subscribe(args => Text = ProcessTextInput(args));
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

        public FontWeight FontWeight
        {
            get { return (FontWeight)GetValue(FontWeightProperty); }
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
            get { return textBlock.Text; }
            set
            {
                textBlock.Text = value;
            }
        }

        public TextWrapping TextWrapping { get; set; }
    }
}