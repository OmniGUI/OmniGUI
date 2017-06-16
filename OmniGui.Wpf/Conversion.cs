using System;
using System.IO;
using System.Windows.Media.Imaging;
using OmniGui.Xaml;
using OmniXaml.Attributes;
using OmniXaml;

namespace OmniGui.Wpf
{
    public class Conversion
    {
        [TypeConverterMember(typeof(Bitmap))]
        public static Func<string, ConvertContext, (bool, object)> ThicknessConverter = (str, v) => (false, GetBitmap(str));

        private ITypeLocator locator;

        private static Bitmap GetBitmap(string str)
        {
            var bitmap = new BitmapImage();

            using (var stream = File.OpenRead(str))
            {
                bitmap.BeginInit();
                bitmap.StreamSource = stream;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
            }

            var frame = BitmapFrame.Create(bitmap);
            var buffer = new byte[frame.PixelWidth * frame.PixelHeight * 4];
            frame.CopyPixels(buffer, frame.PixelWidth * 4, 0);

            return new Bitmap(frame.PixelWidth, frame.PixelHeight, buffer);
        }
    }
}