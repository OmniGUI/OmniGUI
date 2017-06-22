using System;
using System.Drawing;
using Gdk;
using OmniGui.Geometry;
using Font = System.Drawing.Font;
using Point = OmniGui.Geometry.Point;
using Graphics = Gtk.DotNet.Graphics;

namespace OmniGui.Gtk
{
    public class GtkDrawingContext : IDrawingContext
    {
        private readonly EventExpose evnt;

        public GtkDrawingContext(EventExpose evnt)
        {
            this.evnt = evnt;
        }

        public void DrawRectangle(Rect rect, Pen pen)
        {
            using (var g = Graphics.FromDrawable(evnt.Window))
            {
                g.DrawRectangle(pen.ToGtk(), rect.ToGtk());
            }
        }

        public void FillRectangle(Rect rect, Brush brush)
        {
            using (var g = Graphics.FromDrawable(evnt.Window))
            {
                g.FillRectangle(brush.ToGtk(), rect.ToGtk());
            }
        }

        public void DrawRoundedRectangle(Rect rect, Pen pen, CornerRadius cornerRadius)
        {
            using (var g = Graphics.FromDrawable(evnt.Window))
            {
                g.DrawRectangle(pen.ToGtk(), rect.ToGtk());                
            }
        }

        public void FillRoundedRectangle(Rect rect, Brush brush, CornerRadius cornerRadius)
        {
            using (var g = Graphics.FromDrawable(evnt.Window))
            {
                g.FillRectangle(brush.ToGtk(), rect.ToGtk());
            }
        }

        public void DrawText(FormattedText formattedText, Point point, Rect? clipRegion = null)
        {
            using (var g = Graphics.FromDrawable(evnt.Window))
            {
                var font = new Font(new FontFamily(formattedText.FontName), formattedText.FontSize);
                g.DrawString(formattedText.Text, font, formattedText.Brush.ToGtk(), point.ToDrawing());
            }
        }

        public void DrawBitmap(Bitmap bmp, Rect sourceRect, Rect rect)
        {
        }

        public void DrawLine(Point startPoint, Point endPoint, Pen pen)
        {
            using (var g = Graphics.FromDrawable(evnt.Window))
            {
                g.DrawLine(pen.ToGtk(), startPoint.ToDrawing(), startPoint.ToDrawing());
            }
        }
    }
}