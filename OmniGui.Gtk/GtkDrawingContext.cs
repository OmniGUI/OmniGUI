using System.Drawing;
using OmniGui.Geometry;
using Font = System.Drawing.Font;
using Point = OmniGui.Geometry.Point;

namespace OmniGui.Gtk
{
    public class GtkDrawingContext : IDrawingContext
    {
        private readonly Graphics graphics;

        public GtkDrawingContext(Graphics graphics)
        {
            this.graphics = graphics;
        }

        public void DrawRectangle(Pen pen, Rect rect)
        {
            graphics.DrawRectangle(pen.ToPlatform(), rect.ToPlatform());
        }

        public void FillRectangle(Brush brush, Rect rect)
        {
            graphics.FillRectangle(brush.ToPlatform(), rect.ToPlatform());
        }

        public void DrawRoundedRectangle(Pen pen, Rect rect, CornerRadius cornerRadius)
        {
            graphics.DrawRoundedRectangle(rect.ToPlatform(), (int)cornerRadius.BottomLeft, pen.ToPlatform());
        }

        public void FillRoundedRectangle(Brush brush, Rect rect, CornerRadius cornerRadius)
        {
            graphics.FillRoundedRectangle(rect.ToPlatform(), (int) cornerRadius.BottomLeft, brush.ToPlatform());
        }

        public void DrawText(FormattedText formattedText, Point point, Rect? clipRegion = null)
        {
            var font = new Font(new FontFamily(formattedText.FontName), formattedText.FontSize);
            graphics.DrawString(formattedText.Text, font, formattedText.Brush.ToPlatform(), point.ToDrawing());
        }

        public void DrawBitmap(Bitmap bmp, Rect sourceRect, Rect rect)
        {
            var toDraw = bmp.ToPlatform();
            graphics.DrawImage(toDraw, rect.ToPlatform(), sourceRect.ToPlatform(), GraphicsUnit.Pixel);
        }

        public void DrawLine(Pen pen, Point startPoint, Point endPoint)
        {
            graphics.DrawLine(pen.ToPlatform(), startPoint.ToDrawing(), endPoint.ToDrawing());
        }
    }
}