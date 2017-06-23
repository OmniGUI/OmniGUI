using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using OmniGui.Geometry;
using Point = OmniGui.Geometry.Point;
using Size = OmniGui.Geometry.Size;

namespace OmniGui.Gtk
{
    public static class Conversion
    {
        public static System.Drawing.Bitmap ToPlatform(this Bitmap bmp)
        {
            var arrayHandle = GCHandle.Alloc(bmp.Bytes, GCHandleType.Pinned);
            var destBmp = new System.Drawing.Bitmap(bmp.Width, bmp.Height, bmp.Width * sizeof(int),
                PixelFormat.Format32bppArgb, arrayHandle.AddrOfPinnedObject());
            return destBmp;
        }

        public static Rectangle ToPlatform(this OmniGui.Geometry.Rect rect)
        {
            return new Rectangle((int) rect.X, (int) rect.Y, (int) rect.Width, (int) rect.Height);
        }

        public static System.Drawing.Brush ToPlatform(this Brush brush)
        {
            var color = brush.Color.ToPlatform();
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

        public static System.Drawing.Color ToPlatform(this Color color)
        {
            return System.Drawing.Color.FromArgb(color.Alpha, color.Red, color.Green, color.Blue);
        }

        public static System.Drawing.Pen ToPlatform(this Pen pen)
        {
            var brush = pen.Brush.ToPlatform();
            var penThickness = pen.Thickness;
            return new System.Drawing.Pen(brush, (float)penThickness);
        }
    }
}