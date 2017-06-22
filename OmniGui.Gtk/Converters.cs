using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using OmniGui.Xaml;
using OmniXaml.Attributes;
using OmniXaml;

namespace OmniGui.Gtk
{
    public class Converters
    {
        [TypeConverterMember(typeof(Bitmap))]
        public static Func<string, ConvertContext, (bool, object)> ThicknessConverter = (str, v) => (true, GetBitmap(str));

        private static Bitmap GetBitmap(string str)
        {
            var original = (System.Drawing.Bitmap)Image.FromFile(str);
            var width = original.Width;
            var height = original.Height;

            var bmp32BppArgb = new System.Drawing.Bitmap(width, height, PixelFormat.Format32bppArgb);

            byte[] argbValues;
            using (var graphics = Graphics.FromImage(bmp32BppArgb))
            {
                graphics.DrawImage(original, new Rectangle(0, 0, width, height));

                var bmpData = bmp32BppArgb.LockBits(new Rectangle(0,0, bmp32BppArgb.Width, bmp32BppArgb.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, original.PixelFormat);
                IntPtr ptr = bmpData.Scan0;
                int bytes = Math.Abs(bmpData.Stride) * bmp32BppArgb.Height;
                argbValues = new byte[bytes];
                Marshal.Copy(ptr, argbValues, 0, bytes);
                bmp32BppArgb.UnlockBits(bmpData);
                bmp32BppArgb.Dispose();
            }
            
            return new Bitmap(width, height, argbValues);
        }
    }
}