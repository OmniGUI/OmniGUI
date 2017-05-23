namespace OmniGui.Layouts
{
    using System;
    using Zafiro.PropertySystem.Standard;

    public class TextBox : Layout
    {
        public static readonly ExtendedProperty FontSizeProperty = OmniGuiPlatform.PropertyEngine.RegisterProperty("FontSize", typeof(TextBox), typeof(float), new PropertyMetadata { DefaultValue = 16F });
        public static readonly ExtendedProperty FontWeightProperty = OmniGuiPlatform.PropertyEngine.RegisterProperty("FontWeight", typeof(TextBox), typeof(float), new PropertyMetadata { DefaultValue = FontWeights.Normal });
        public static readonly ExtendedProperty FontFamilyProperty = OmniGuiPlatform.PropertyEngine.RegisterProperty("FontFamily", typeof(TextBox), typeof(float), new PropertyMetadata { DefaultValue = "Arial" });
        public static readonly ExtendedProperty TextProperty = OmniGuiPlatform.PropertyEngine.RegisterProperty("Text", typeof(TextBox), typeof(string), new PropertyMetadata { DefaultValue = null });
        public static readonly ExtendedProperty ForegroundProperty = OmniGuiPlatform.PropertyEngine.RegisterProperty("Foreground", typeof(TextBox), typeof(Brush), new PropertyMetadata { DefaultValue = new Brush(Colors.Black) });
        public static readonly ExtendedProperty TextWrappingProperty = OmniGuiPlatform.PropertyEngine.RegisterProperty("TextWrapping", typeof(TextBox), typeof(TextWrapping), new PropertyMetadata { DefaultValue = TextWrapping.NoWrap });

        public TextBox()
        {
            NotifyRenderAffectedBy(TextProperty);
            GetChangedObservable(TextProperty).Subscribe(t => Text = (string) t);
            Children.OnChildAdded(AttachToTextBoxView);            
        }

        private void AttachToTextBoxView(Layout child)
        {
            var textBoxView = child.FindChild<TextBoxView>();
            textBoxView.GetChangedObservable(TextBoxView.TextProperty).Subscribe(o => Text = (string)o);
            GetChangedObservable(TextProperty).Subscribe(o => textBoxView.Text = (string) o);
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
            get { return (Brush) GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        public TextWrapping TextWrapping
        {
            get { return (TextWrapping)GetValue(TextWrappingProperty); }
            set
            {
                SetValue(TextProperty, value);
            }
        }
    }
}