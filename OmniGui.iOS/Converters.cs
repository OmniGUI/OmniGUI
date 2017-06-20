using OmniGui.Xaml;
using OmniXaml;
using OmniXaml.Attributes;
using System;
using CoreGraphics;
using UIKit;

namespace OmniGui.iOS
{
    public static class Converter
    {
        [TypeConverterMember(typeof(Bitmap))]
        public static Func<string, ConvertContext, (bool, object)> ThicknessConverter = (str, v) => (true, GetBitmap(str));
        
        private static Bitmap GetBitmap(string path)
        {
            using (var stream = path.OpenStream())
            {
                var buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                var provider = new CGDataProvider(buffer);
                var image = CGImage.FromPNG(provider, null, false, CGColorRenderingIntent.Default);

                int width = (int)image.Width;
                int height = (int)image.Height;
                CGColorSpace colorSpace = CGColorSpace.CreateDeviceRGB();
                byte[] rawData = new byte[height * width * 4];
                int bytesPerPixel = 4;
                int bytesPerRow = bytesPerPixel * width;
                int bitsPerComponent = 8;
                CGContext context = new CGBitmapContext(rawData, width, height,
                    bitsPerComponent, bytesPerRow, colorSpace,
                    CGBitmapFlags.PremultipliedLast | CGBitmapFlags.ByteOrder32Big);

                context.DrawImage(new CGRect(0, 0, width, height), image);

                return new Bitmap((int)image.Width, (int)image.Height, rawData);
            }
            
        }
    }
}