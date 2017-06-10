using System;
using System.Threading.Tasks;
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

            ActionBar.Hide();
            Window.AddFlags(WindowManagerFlags.Fullscreen);

            SetContentView(new OmniGuiView(this)
            {
                Source = "Layout.xaml",
                DataContext = new SampleViewModel(new AndroidMessageService())
            });
        }

        //private string ReadTextFromAsset(string assetName)
        //{
        //    using (var reader = new StreamReader(Assets.Open(assetName)))
        //    {
        //        return reader.ReadToEnd();
        //    }
        //}

        //private Layout LoadLayout(string xaml)
        //{
        //    var assemblies = new[]
        //    {
        //        Assembly.Load(new AssemblyName("OmniGui")),
        //        Assembly.Load(new AssemblyName("OmniGui.Xaml")),
        //        Assembly.Load(new AssemblyName("AndroidApp")),
        //        Assembly.Load(new AssemblyName("Common")),
        //    };

        //    var locator = new TypeLocator(() => ControlTemplates, null);
        //    var xamlLoader = new OmniGuiXamlLoader(assemblies, () => ControlTemplates, locator);

        //    var loadXaml = xamlLoader.Load(xaml);
        //    var container = (Container)xamlLoader.Load(ReadTextFromAsset("Container.xaml"));

        //    ControlTemplates = container.ControlTemplates;

        //    var layout = (Layout) loadXaml;
        //    new TemplateInflator().Inflate(layout, ControlTemplates);

        //    return layout;
        //}

        //public ICollection<ControlTemplate> ControlTemplates { get; set; }

        //[TypeConverterMember(typeof(ICommand))]
        //public static Func<string, ConvertContext, (bool, object)> CommandConverter = (str, v) => (true, new DelegateCommand(() =>
        //{
        //    new AlertDialog.Builder(Application.Context)
        //    .SetTitle("Salutations")
        //    .SetMessage("Se me va la vida a puñaos")
        //    .Show();
        //}));
    }

    public class AndroidMessageService : IMessageService
    {
        public Task<int> ShowMessage(string message)
        {
            throw new NotImplementedException();
        }
    }
}
