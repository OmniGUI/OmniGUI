namespace OmniGui
{
    using Geometry;

    public class FormattedText
    {
        public ITextEngine TextEngine { get; }

        public FormattedText(ITextEngine textEngine)
        {
            TextEngine = textEngine;
        }

        public FormattedText(FormattedText formattedText, ITextEngine textEngine) : this(textEngine)
        {
            Text = formattedText.Text;
            FontWeight = formattedText.FontWeight;
            FontSize = formattedText.FontSize;
            Constraint = formattedText.Constraint;
            Brush = formattedText.Brush;
            FontName = formattedText.FontName;
        }

        public string Text { get; set; }
        public string FontName { get; set; }
        public Brush Brush { get; set; }
        public Size Constraint { get; set; }
        public float FontSize { get; set; }
        public FontWeights FontWeight { get; set; }

        public Size DesiredSize => TextEngine.Measure(this);

        public Size Measure()
        {
            return TextEngine.Measure(this);
        }
    }
}