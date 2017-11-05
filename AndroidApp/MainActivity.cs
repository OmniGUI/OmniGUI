using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Util;
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

            AttachUnhandleExceptions();

            AndroidPlatform.Current.Assets = Assets;

            ActionBar.Hide();
            Window.AddFlags(WindowManagerFlags.Fullscreen);

            var assemblies = typeof(MainActivity).Assembly
                .GetReferencedAssemblies()
                .Concat(new[] { new AssemblyName("OmniGui.Xaml"), new AssemblyName("OmniGui") })
                .Select(Assembly.Load)
                .ToList();

            SetContentView(new OmniGuiView(this, assemblies)
            {
                Source = "layout.xaml",
                DataContext = new SampleViewModel(new AndroidMessageService(this))
            });
        }

        private static void AttachUnhandleExceptions()
        {
            AndroidEnvironment.UnhandledExceptionRaiser += (sender, args) =>
            {
                Log.Error("UnhandledExceptionRaiser", args.Exception.ToString());
                args.Handled = true;
            };

            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                Log.Error("UnhandledException", args.ExceptionObject.ToString());

            TaskScheduler.UnobservedTaskException += (sender, args) =>
                Log.Error("UnobservedTaskException", args.Exception.ToString());
        }
    }
}
