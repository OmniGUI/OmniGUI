using System;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using OmniGui.Xaml;
using OmniXaml;
using OmniXaml.Attributes;
using Serilog;

namespace OmniGui.Uwp
{
    public class Converters
    {
        [TypeConverterMember(typeof(Bitmap))]
        public static Func<string, ConvertContext, (bool, object)> BitmapConverter = (str, v) => (true, GetValue(str));

        private static Bitmap GetValue(string str)
        {
            Log.Information("Creating bitmap {Bitmap}...", str);
            var uri = new Uri($"ms-appx:///{str}");
            var bitmap = Task.Run(() => GetBitmapFromUri(uri)).Result;
            Log.Information("Created bitmap {Bitmap}", str);
            return bitmap;
        }

        private static async Task<Bitmap> GetBitmapFromUri(Uri imageName)
        {
            var storageFile = await StorageFile.GetFileFromApplicationUriAsync(imageName);
            var stream = await storageFile.OpenAsync(FileAccessMode.Read);
            using (stream)
            {
                return await GetBitmapFromStream(stream);
            }
        }

        private static async Task<Bitmap> GetBitmapFromStream(IRandomAccessStream stream)
        {
            var decoder = await BitmapDecoder.CreateAsync(BitmapDecoder.PngDecoderId, stream);
            var frame = await decoder.GetFrameAsync(0);
            var pixelData = await frame.GetPixelDataAsync();
            var bytes = pixelData.DetachPixelData();

            return new Bitmap((int)frame.PixelWidth, (int)frame.PixelHeight, bytes);
        }
    }
}