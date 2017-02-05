namespace OmniGui
{
    public interface ITextEngine
    {
        Size Measure(FormattedText formattedText);
        double GetHeight(string fontFamily);
    }
}