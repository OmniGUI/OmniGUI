using System;
using System.IO;
using System.Reflection;
using System.Windows.Input;
using Android.App;
using Android.OS;
using Android.Widget;
using AndroidApp.AndPlugin;
using OmniGui;
using OmniXaml;
using OmniXaml.Services;
using OmniXaml.Attributes;
using WpfApp;

namespace AndroidApp
{
    [Activity(Label = "AndroidApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            var xamlLoader = new XamlLoader(new[]
            {
                Assembly.Load(new AssemblyName("OmniGui")),
                Assembly.Load(new AssemblyName("OmniGui.Xaml")),
                Assembly.Load(new AssemblyName("AndroidApp")),
                Assembly.Load(new AssemblyName("ViewModels")),
            });

            string xaml;
            using (var r = new StreamReader(Assets.Open("layout.xaml")))
            {
                xaml = r.ReadToEnd();
            }
            
            var layout = (Layout) xamlLoader.Load(xaml).Instance;

            var omniGuiView = new OmniGuiView(ApplicationContext, layout);

            Platform.Current.TextEngine = new AndroidTextEngine();
            Platform.Current.EventDriver = new AndroidEventProcessor(omniGuiView);
            SetContentView(omniGuiView);
        }

        [TypeConverterMember(typeof(ICommand))]
        public static Func<ConverterValueContext, object> CommandConverter = context => new DelegateCommand(() =>
        {
            new AlertDialog.Builder(Application.Context)
            .SetTitle("Salutations")
            .SetMessage("Se me va la vida a puñaos")
            .Show();
        });
    } 
}