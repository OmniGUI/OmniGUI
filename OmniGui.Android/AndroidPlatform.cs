using Android.Content.Res;

namespace OmniGui.Android
{
    public class AndroidPlatform
    {
        public AssetManager Assets { get; set; }
        public static AndroidPlatform Current { get; set; } = new AndroidPlatform();
    }
}