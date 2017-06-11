namespace OmniGui
{
    using Geometry;

    public interface ITextEngine
    {
        Size Measure(FormattedText formattedText);
        double GetHeight(string fontName, float fontSize);
    }
}