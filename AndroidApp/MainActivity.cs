using Android.App;
using Android.OS;
using Android.Views;
using Common;
using OmniGui;
using OmniGui.Android;

namespace AndroidApp
{
    [Activity(Label = "AndroidApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            OmniGuiPlatform.PropertyEngine = new OmniGuiPropertyEngine();
            AndroidPlatform.Current.Assets = Assets;

            ActionBar.Hide();
            Window.AddFlags(WindowManagerFlags.Fullscreen);
            
            SetContentView(new OmniGuiView(this)
            {
                Source = "Layout.xaml",
                DataContext = new SampleViewModel(new AndroidMessageService(this))
            });            
        }        
    }
}
