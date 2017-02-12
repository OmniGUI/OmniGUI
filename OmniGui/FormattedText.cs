namespace OmniGui
{
    using Space;

    public class FormattedText
    {
        public string Text { get; set; }
        public string FontFamily { get; set; }
        public string FontName { get; set; }
        public Brush Brush { get; set; }
        public Size Constraint { get; set; }
        public float FontSize { get; set; }
        public FontWeights FontWeight { get; set; }

        public Size Measure()
        {
            return Platform.Current.TextEngine.Measure(this);
        }
    }
}