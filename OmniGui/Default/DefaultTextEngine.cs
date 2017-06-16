using OmniGui.Geometry;

namespace OmniGui.Default
{
    public class DefaultTextEngine : ITextEngine
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