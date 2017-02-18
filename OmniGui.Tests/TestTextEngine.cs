namespace OmniGui.Tests
{
    using Geometry;

    public class TestTextEngine : ITextEngine
    {
        public Size Measure(FormattedText formattedText)
        {
            return Size.Empty;
        }

        public double GetHeight(string fontFamily)
        {
            return 0;
        }
    }
}