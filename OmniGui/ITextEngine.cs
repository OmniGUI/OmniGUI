namespace OmniGui
{
    public interface ITextEngine
    {
        Size MeasureText(string text);
        Size Measure(FormattedText formattedText);
    }
}