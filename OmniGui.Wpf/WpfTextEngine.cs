namespace OmniGui.Wpf
{
    using System;
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

        public double GetHeight(string fontFamilyName)
        {
            var fontFamily = new FontFamily(fontFamilyName);
            var fontDpiSize = 16;
            var fontHeight = Math.Ceiling(fontDpiSize * fontFamily.LineSpacing);
            return fontHeight;
        }
    }
}