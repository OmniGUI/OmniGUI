using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace OmniGui.Gtk
{
    public static class GraphicsMixin
    {
        private const double PenMinWidthTolerance = 0.1;

        public static void DrawRoundedRectangle(this Graphics graphics, Rectangle rect, int radius, System.Drawing.Pen pen)
        {
            if (Math.Abs(pen.Width) < PenMinWidthTolerance)
            {
                return;
            }

            if (radius == 0)
            {
                graphics.DrawRectangle(pen, rect);
            }
            else
            {
                var path = GetRoundRectanglePath(rect, radius);

                graphics.DrawPath(pen, path);
            }            
        }

        public static void FillRoundedRectangle(this Graphics graphics, Rectangle r, int radius, System.Drawing.Brush brush)
        {
            if (radius == 0)
            {
                graphics.FillRectangle(brush, r);
            }
            else
            {
                var path = GetRoundRectanglePath(r, radius);

                graphics.FillPath(brush, path);
            }
        }

        private static GraphicsPath GetRoundRectanglePath(Rectangle rect, int cornerRadius)
        {
            var path = new GraphicsPath();

            path.AddArc(rect.X, rect.Y, cornerRadius, cornerRadius, 180, 90);
            path.AddArc(rect.X + rect.Width - cornerRadius, rect.Y, cornerRadius, cornerRadius, 270, 90);
            path.AddArc(rect.X + rect.Width - cornerRadius, rect.Y + rect.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            path.AddLine(rect.X, rect.Y + rect.Height - cornerRadius, rect.X, rect.Y + cornerRadius / 2);
            return path;
        }
    }
}