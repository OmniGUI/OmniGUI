using System.IO;
using Android.Content.Res;

namespace OmniGui.Android
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

        public static Stream ReadStreamFromAsset(string assetName, AssetManager assetManager)
        {
            return assetManager.Open(assetName);
        }
    }
}