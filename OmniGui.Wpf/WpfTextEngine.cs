namespace OmniGui.Wpf
{
    using System.Windows.Media;
    using FormattedText = OmniGui.FormattedText;

    public class WpfTextEngine : ITextEngine
    {
        public Size Measure(FormattedText formattedText)
        {
            if (formattedText.Text == null)
            {
                return Size.Empty;
            }

            var ft = formattedText.ToWpf();
            return new Size(ft.Width, ft.Height);
        }

        public double GetHeight(string fontFamily)
        {
            return new FontFamily(fontFamily).Baseline;
        }
    }
}