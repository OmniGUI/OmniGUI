using System.Drawing;
using OmniGui.Geometry;
using Point = OmniGui.Geometry.Point;
using Size = OmniGui.Geometry.Size;

namespace OmniGui.Gtk
{
    public static class Conversion
    {
        public static Rectangle ToGtk(this OmniGui.Geometry.Rect rect)
        {
            return new Rectangle((int) rect.X, (int) rect.Y, (int) rect.Width, (int) rect.Height);
        }

        public static System.Drawing.Brush ToGtk(this Brush brush)
        {
            var color = brush.Color.ToGtk();
            return new SolidBrush(color);
        }

        public static Rect ToOmniGui(this Rectangle rect)
        {
            return new Rect(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static Size ToOmniGui(this Gdk.Size size)
        {
            return new Size(size.Width, size.Height);
        }

        public static Size ToOmniGui(this System.Drawing.Size size)
        {
            return new Size(size.Width, size.Height);
        }

        public static Size ToOmniGui(this SizeF size)
        {
            return new Size(size.Width, size.Height);
        }

        public static SizeF ToDrawing(this Size size)
        {
            return new SizeF((float) size.Width, (float) size.Height);
        }

        public static PointF ToDrawing(this Point point)
        {
            return new PointF((float)point.X, (float)point.Y);
        }

        public static System.Drawing.Color ToGtk(this Color color)
        {
            return System.Drawing.Color.FromArgb(color.Alpha, color.Red, color.Green, color.Blue);
        }

        public static System.Drawing.Pen ToGtk(this Pen pen)
        {
            var brush = pen.Brush.ToGtk();
            var penThickness = pen.Thickness;
            return new System.Drawing.Pen(brush, (float)penThickness);
        }
    }
}