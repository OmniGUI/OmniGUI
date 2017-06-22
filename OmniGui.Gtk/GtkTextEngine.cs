using System;
using System.Drawing;
using Size = OmniGui.Geometry.Size;

namespace OmniGui.Gtk
{
    internal class GtkTextEngine : ITextEngine
    {
        public Graphics Graphics { get; set; }

        public Size Measure(FormattedText formattedText)
        {
            var font = new Font(new FontFamily(formattedText.FontName), formattedText.FontSize);
            var layoutArea = formattedText.Constraint.ToDrawing();
            var size = Graphics.MeasureString(formattedText.Text, font, layoutArea);

            return size.ToOmniGui();
        }

        public double GetHeight(string fontName, float fontSize)
        {
            var font = new Font(new FontFamily(fontName), fontSize);
            return font.GetHeight();
        }
    }
}