namespace OmniGui
{
    using Geometry;

    public class FormattedText
    {
        public FormattedText()
        {            
        }

        public FormattedText(FormattedText formattedText)
        {
            Text = formattedText.Text;
            FontFamily = formattedText.FontFamily;
            FontWeight = formattedText.FontWeight;
            FontSize = formattedText.FontSize;
            Constraint = formattedText.Constraint;
            Brush = formattedText.Brush;
            FontName = formattedText.FontName;
        }

        public string Text { get; set; }
        public string FontFamily { get; set; }
        public string FontName { get; set; }
        public Brush Brush { get; set; }
        public Size Constraint { get; set; }
        public float FontSize { get; set; }
        public FontWeights FontWeight { get; set; }

        public Size DesiredSize => Platform.Current.TextEngine.Measure(this);

        public Size Measure()
        {
            return Platform.Current.TextEngine.Measure(this);
        }
    }
}