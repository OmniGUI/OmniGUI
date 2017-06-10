using System.IO;
using Android.Content.Res;

namespace AndroidApp.AndPlugin
{
    public static class ReadMixin
    {
        public static string ReadTextFromAsset(string assetName, AssetManager assetManager)
        {
            using (var reader = new StreamReader(assetManager.Open(assetName)))
            {
                return reader.ReadToEnd();
            }
        }
    }
}