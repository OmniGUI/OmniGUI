using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Gdk;
using OmniGui.Geometry;
using Font = System.Drawing.Font;
using Point = OmniGui.Geometry.Point;

namespace OmniGui.Gtk
{
    public class GtkDrawingContext : IDrawingContext
    {
        private readonly Graphics g;
        private readonly EventExpose evnt;

        public GtkDrawingContext(EventExpose evnt)
        {
            this.evnt = evnt;
        }

        public GtkDrawingContext(Graphics g)
        {
            this.g = g;
        }

        public void DrawRectangle(Pen pen, Rect rect)
        {
            g.DrawRectangle(pen.ToPlatform(), rect.ToPlatform());
        }

        public void FillRectangle(Brush brush, Rect rect)
        {
            g.FillRectangle(brush.ToPlatform(), rect.ToPlatform());
        }

        public void DrawRoundedRectangle(Pen pen, Rect rect, CornerRadius cornerRadius)
        {
            g.DrawRoundedRectangle(rect.ToPlatform(), (int)cornerRadius.BottomLeft, pen.ToPlatform());
        }

        public void FillRoundedRectangle(Brush brush, Rect rect, CornerRadius cornerRadius)
        {
            g.FillRoundedRectangle(rect.ToPlatform(), (int) cornerRadius.BottomLeft, brush.ToPlatform());
        }

        public void DrawText(FormattedText formattedText, Point point, Rect? clipRegion = null)
        {
            var font = new Font(new FontFamily(formattedText.FontName), formattedText.FontSize);
            g.DrawString(formattedText.Text, font, formattedText.Brush.ToPlatform(), point.ToDrawing());
        }

        public void DrawBitmap(Bitmap bmp, Rect sourceRect, Rect rect)
        {
            var toDraw = bmp.ToPlatform();
            g.DrawImage(toDraw, rect.ToPlatform(), sourceRect.ToPlatform(), GraphicsUnit.Pixel);
        }

        public void DrawLine(Pen pen, Point startPoint, Point endPoint)
        {
            g.DrawLine(pen.ToPlatform(), startPoint.ToDrawing(), endPoint.ToDrawing());
        }
    }
}