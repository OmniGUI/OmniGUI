using CoreGraphics;
using OmniGui.Geometry;
using UIKit;

namespace OmniGui.iOS
{
    public static class Conversion
    {
        public static Rect ToOmniGui(this CGRect rect)
        {
            return new Rect(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static CGRect ToiOS(this Rect rect)
        {
            return new CGRect(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static CGSize ToiOS(this Size size)
        {
            return new CGSize(size.Width, size.Height);
        }

        public static Size ToOmniGui(this CGSize size)
        {
            return new Size(size.Width, size.Height);
        }

        public static CGColor ToiOS(this Color color)
        {
            var c = UIColor.FromRGBA(color.Red, color.Green, color.Blue, color.Alpha);
            return c.CGColor;
        }

        public static CGPoint ToiOS(this Point point)
        {
            return new CGPoint(point.X, point.Y);
        }

        public static CGImage ToiOS(this Bitmap bmp)
        {
            int bytesPerPixel = 4;
            var width = bmp.Width;
            int bytesPerRow = bytesPerPixel * width;
            int bitsPerComponent = 8;

            var rawData = bmp.Bytes;
            var height = bmp.Height;
            var colorSpace = CGColorSpace.CreateDeviceRGB();
            
            var bmpContext = new CGBitmapContext(rawData, width, height,
                bitsPerComponent, bytesPerRow, colorSpace,
                CGBitmapFlags.PremultipliedFirst | CGBitmapFlags.ByteOrder32Big); 
            
            return bmpContext.ToImage();
        }
    }
}