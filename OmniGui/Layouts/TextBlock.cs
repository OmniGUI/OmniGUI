namespace OmniGui.Layouts
{
    using System;
    using Geometry;
    using Zafiro.PropertySystem.Standard;

    public class TextBlock : Layout
    {
        private Brush foreground;

        public static readonly ExtendedProperty FontSizeProperty = PropertyEngine.RegisterProperty("FontSize", typeof(TextBlock),
            typeof(float), new PropertyMetadata { DefaultValue = 14F });

        public static readonly ExtendedProperty FontWeightProperty = PropertyEngine.RegisterProperty("FontWeight", typeof(TextBlock),
    typeof(float), new PropertyMetadata { DefaultValue = FontWeights.Normal });

        public static readonly ExtendedProperty FontFamilyProperty = PropertyEngine.RegisterProperty("FontFamily", typeof(TextBlock),
typeof(float), new PropertyMetadata { DefaultValue = "Arial" });

        public static readonly ExtendedProperty TextProperty = PropertyEngine.RegisterProperty("Text", typeof(TextBlock),
typeof(string), new PropertyMetadata { DefaultValue = null });

        private string currentText;

        public TextBlock()
        {
            Foreground = new Brush(Colors.Black);
            GetChangedObservable(TextProperty).Subscribe(t => Text = (string) t);
            NotifyRenderAffectedBy(TextProperty);                        
        }

        private void UpdateFormattedText()
        {
            FormattedText.Text = Text;
            FormattedText.FontFamily = FontFamily;
            FormattedText.Brush = Foreground;
            FormattedText.FontSize = FontSize;
            FormattedText.FontWeight = FontWeight;
        }

        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set
            {
                SetValue(FontFamilyProperty, value);
                UpdateFormattedText();
            }
        }

        public FontWeights FontWeight
        {
            get { return (FontWeights)GetValue(FontWeightProperty); }
            set
            {
                SetValue(FontWeightProperty, value);
                UpdateFormattedText();
            }
        }

        public float FontSize
        {
            get { return (float)GetValue(FontSizeProperty); }
            set
            {
                SetValue(FontSizeProperty, value);
                UpdateFormattedText();
            }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (Text == null)
            {
                var height = Platform.Current.TextEngine.GetHeight(FontFamily);
                return new Size(0, height);
            }

            if (TextWrapping == TextWrapping.Wrap)
            {
                FormattedText.Constraint = new Size(availableSize.Width, double.PositiveInfinity);
            }
            else
            {
                FormattedText.Constraint = Size.Infinite;
            }

            return FormattedText.Measure();
        }

        public override void Render(IDrawingContext drawingContext)
        {
            if (Text == null)
            {
                return;
            }

            drawingContext.DrawText(FormattedText, VisualBounds.Point);
        }

        public Brush Foreground
        {
            get { return foreground; }
            set
            {
                foreground = value;
                UpdateFormattedText();
            }
        }

        public string Text
        {
            get { return currentText; }
            set
            {
                currentText = value;
                UpdateFormattedText();
                var subject = GetObserver(TextProperty);
                subject.OnNext(value);                
            }
        }

        private FormattedText FormattedText { get; set; } = new FormattedText() { FontSize = 16 };

        public TextWrapping TextWrapping { get; set; }
    }
}