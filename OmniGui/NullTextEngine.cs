namespace OmniGui
{
    public class NullTextEngine : ITextEngine
    {
        public Size Measure(FormattedText formattedText)
        {
            return new Size();
        }

        public double GetHeight(string fontFamily)
        {
            return 0D;
        }
    }
}