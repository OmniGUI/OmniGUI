using OmniGui;
using OmniGui.Geometry;

namespace iOSApp.Omni
{
    public class iOSTextEngine : ITextEngine
    {
        public Size Measure(FormattedText formattedText)
        {
            return Size.Empty;
        }

        public double GetHeight(string fontFamily, float fontSize)
        {
            return 10D;
        }
    }
}