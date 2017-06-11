namespace OmniGui.Wpf
{
    using System;
    using System.Windows.Media;
    using Geometry;
    using FormattedText = FormattedText;

    public class WpfTextEngine : ITextEngine
    {
        public Size Measure(FormattedText formattedText)
        {
            if (formattedText.Text == null)
            {
                return Size.Empty;
            }

            var ft = formattedText.ToWpf();
            return new Size(ft.WidthIncludingTrailingWhitespace, ft.Height);
        }

        public double GetHeight(string fontName, float fontSize)
        {
            var fontFamily = new FontFamily(fontName);
            var fontDpiSize = 16;
            var fontHeight = Math.Ceiling(fontDpiSize * fontFamily.LineSpacing);
            return fontHeight;
        }
    }
}