namespace OmniGui.Layouts
{
    using System;
    using System.Runtime.InteropServices.ComTypes;
    using Zafiro.PropertySystem;
    using Zafiro.PropertySystem.Standard;

    public class TextBox : Layout
    {
        public static ExtendedProperty FontSizeProperty;
        public static ExtendedProperty FontWeightProperty;
        public static ExtendedProperty FontFamilyProperty;
        public static ExtendedProperty TextProperty;
        public static ExtendedProperty ForegroundProperty;
        public static ExtendedProperty TextWrappingProperty;

        public TextBox(IPropertyEngine propertyEngine) : base(propertyEngine)
        {
            RegistrationGuard.RegisterFor<TextBox>(() =>
            {
                TextWrappingProperty = PropertyEngine.RegisterProperty("TextWrapping", typeof(TextBox), typeof(TextWrapping), new PropertyMetadata { DefaultValue = TextWrapping.NoWrap });
                ForegroundProperty = PropertyEngine.RegisterProperty("Foreground", typeof(TextBox), typeof(Brush), new PropertyMetadata { DefaultValue = new Brush(Colors.Black) });
                TextProperty = PropertyEngine.RegisterProperty("Text", typeof(TextBox), typeof(string), new PropertyMetadata { DefaultValue = null });
                FontFamilyProperty = PropertyEngine.RegisterProperty("FontFamily", typeof(TextBox), typeof(float), new PropertyMetadata { DefaultValue = "Arial" });
                FontWeightProperty = PropertyEngine.RegisterProperty("FontWeight", typeof(TextBox), typeof(float), new PropertyMetadata { DefaultValue = FontWeights.Normal });
                FontSizeProperty = PropertyEngine.RegisterProperty("FontSize", typeof(TextBox), typeof(float), new PropertyMetadata { DefaultValue = 16F });
            });
            
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