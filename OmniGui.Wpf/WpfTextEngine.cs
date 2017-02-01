namespace OmniGui.Wpf
{
    public class WpfTextEngine : ITextEngine
    {
        public Size Measure(FormattedText formattedText)
        {
            var ft = formattedText.ToWpf();
            return new Size(ft.Width, ft.Height);
        }
    }
}