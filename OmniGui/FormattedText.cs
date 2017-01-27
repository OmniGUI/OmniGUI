namespace OmniGui
{
    public class FormattedText
    {
        public string Text { get; set; }
        public Brush Brush { get; set; }
        public Size Constraint { get; set; } 

        public Size Measure()
        {
            return Platform.Current.TextEngine.Measure(this);
        }
    }
}