using System;
using OmniGui.Geometry;
using Zafiro.PropertySystem.Standard;
using OmniXaml.Attributes;

namespace OmniGui.Layouts
{
    public class TextBlock : Layout
    {
        public static ExtendedProperty FontSizeProperty = OmniGuiPlatform.PropertyEngine.RegisterProperty("FontSize",
            typeof(TextBlock), typeof(float), new PropertyMetadata {DefaultValue = 16F});

        public static ExtendedProperty FontWeightProperty =
            OmniGuiPlatform.PropertyEngine.RegisterProperty("FontWeight", typeof(TextBlock), typeof(float),
                new PropertyMetadata {DefaultValue = FontWeights.Normal});

        public static ExtendedProperty FontNameProperty = OmniGuiPlatform.PropertyEngine.RegisterProperty("FontName",
            typeof(TextBlock), typeof(float), new PropertyMetadata {DefaultValue = "Arial"});

        public static ExtendedProperty TextProperty = OmniGuiPlatform.PropertyEngine.RegisterProperty("Text",
            typeof(TextBlock), typeof(string), new PropertyMetadata {DefaultValue = null});

        private string currentText;
        private Brush foreground;

        public TextBlock(FrameworkDependencies deps) : base(deps)
        {
            FormattedText = new FormattedText(Deps.TextEngine) {FontSize = 16};
            Foreground = new Brush(Colors.Black);
            GetChangedObservable(TextProperty).Subscribe(t => Text = (string) t);
            NotifyRenderAffectedBy(TextProperty);
        }

        public string FontName
        {
            get => (string) GetValue(FontNameProperty);
            set
            {
                SetValue(FontNameProperty, value);
                UpdateFormattedText();
            }
        }

        public FontWeights FontWeight
        {
            get => (FontWeights) GetValue(FontWeightProperty);
            set
            {
                SetValue(FontWeightProperty, value);
                UpdateFormattedText();
            }
        }

        public float FontSize
        {
            get => (float) GetValue(FontSizeProperty);
            set
            {
                SetValue(FontSizeProperty, value);
                UpdateFormattedText();
            }
        }

        public Brush Foreground
        {
            get => foreground;
            set
            {
                foreground = value;
                UpdateFormattedText();
            }
        }

        [Content]
        public string Text
        {
            get => currentText;
            set
            {
                currentText = value;
                UpdateFormattedText();
                var subject = GetObserver(TextProperty);
                subject.OnNext(value);
            }
        }

        private FormattedText FormattedText { get; }

        public TextWrapping TextWrapping { get; set; }

        private void UpdateFormattedText()
        {
            FormattedText.Text = Text;
            FormattedText.Brush = Foreground;
            FormattedText.FontSize = FontSize;
            FormattedText.FontWeight = FontWeight;
            FormattedText.FontName = FontName;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (Text == null)
            {
                var height = Deps.TextEngine.GetHeight(FontName, FontSize);
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
    }
}