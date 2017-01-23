using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace OmniGui.Wpf
{
    public class WpfTextEngine : ITextEngine
    {
        public Size MeasureText(string text)
        {
            var brush = new Brush(Colors.Black);
            var formattedText = new System.Windows.Media.FormattedText(text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 15, brush.ToWpf(), new NumberSubstitution(), 3D);
            return new Size(formattedText.Width, formattedText.Height);
        }

        public Size Measure(FormattedText formattedText)
        {
            var ft = formattedText.ToWpf();
            return new Size(ft.Width, ft.Height);
        }
    }
}