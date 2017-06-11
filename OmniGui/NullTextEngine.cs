namespace OmniGui
{
    using Geometry;

    public class NullTextEngine : ITextEngine
    {
        public Size Measure(FormattedText formattedText)
        {
            return new Size();
        }

        public double GetHeight(string fontName, float fontSize)
        {
            return 0D;
        }
    }
}