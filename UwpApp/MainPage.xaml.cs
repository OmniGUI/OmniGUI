using Windows.UI.Xaml.Controls;

namespace UwpApp
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Windows.ApplicationModel;
    using Windows.Storage;
    using Windows.UI.Xaml;
    using Microsoft.Graphics.Canvas.UI.Xaml;
    using OmniGui;
    using OmniXaml.Services;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Layout layout;

        public MainPage()
        {
            this.InitializeComponent();

          
            Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var xamlLoader = new XamlLoader(new[]
          {
                Assembly.Load(new AssemblyName("OmniGui")),
                Assembly.Load(new AssemblyName("OmniGui.Xaml")),
            });
            var xaml = await GetXaml("Layout.xaml");

            layout = (Layout)xamlLoader.Load(xaml).Instance;
        }


        private void CanvasControl_OnDraw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            if (layout == null)
            {
                return;
            }
        }

        private static async Task<string> GetXaml(string fileName)
        {
            var uri = new Uri($"ms-appx:///{fileName}");
            var file = await StorageFile.GetFileFromApplicationUriAsync(uri);
            var xaml = await FileIO.ReadTextAsync(file);
            return xaml;
        }
    }
}
