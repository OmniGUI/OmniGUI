using System;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using OmniGui.Xaml;
using OmniXaml;
using OmniXaml.Attributes;

namespace OmniGui.Uwp
{
    public class Converters
    {
        [TypeConverterMember(typeof(Bitmap))]
        public static Func<string, ConvertContext, (bool, object)> BitmapConverter = (str, v) => (true, GetValue(str));

        private static Bitmap GetValue(string str)
        {
            var bitmap = GetBitmap(str).Result;
            return bitmap;
        }

        private static async Task<Bitmap> GetBitmap(string imageName)
        {
            var stream = await GetStream(imageName).ConfigureAwait(false);
            using (stream)
            {
                var bitmap = await CreateFromStream(stream);
                return bitmap;
            }
        }

        private static async Task<IRandomAccessStream> GetStream(string fileName)
        {
            var uri = new Uri($"ms-appx:///{fileName}");
            var storageFile = await StorageFile.GetFileFromApplicationUriAsync(uri).AsTask().ConfigureAwait(false);
            return await storageFile.OpenAsync(FileAccessMode.Read).AsTask().ConfigureAwait(false);
        }

        private static async Task<Bitmap> CreateFromStream(IRandomAccessStream stream)
        {
            var decoder = await BitmapDecoder.CreateAsync(BitmapDecoder.PngDecoderId, stream);
            var frame = await decoder.GetFrameAsync(0);
            var pixelData = await frame.GetPixelDataAsync();
            var bytes = pixelData.DetachPixelData();

            return new Bitmap((int)frame.PixelWidth, (int)frame.PixelHeight, bytes);
        }
    }
}