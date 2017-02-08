namespace UwpApp
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Windows.Storage;
    using Windows.UI.Popups;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Microsoft.Graphics.Canvas.UI.Xaml;
    using OmniGui;
    using OmniGui.Xaml;
    using OmniXaml;
    using OmniXaml.Attributes;
    using Plugin;

    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        [TypeConverterMember(typeof(ICommand))] public static Func<ConverterValueContext, object> CommandConverter =
            context => new DelegateCommand(async () => await new MessageDialog("Tapped!!").ShowAsync());

        private readonly Win2DTextEngine textEngine = new Win2DTextEngine();
        private Layout layout;

        public MainPage()
        {
            InitializeComponent();

            Platform.Current.TextEngine = textEngine;
            Platform.Current.EventDriver = new UwpEventProcessor(this, Canvas);

            Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var xamlLoader = new OmniGuiXamlLoader(new[]
            {
                Assembly.Load(new AssemblyName("OmniGui")),
                Assembly.Load(new AssemblyName("OmniGui.Xaml")),
                Assembly.Load(new AssemblyName("UwpApp")),
                Assembly.Load(new AssemblyName("ViewModels"))
            });
            var xaml = await GetXaml("Layout.xaml");

            layout = (Layout) xamlLoader.Load(xaml).Instance;
        }


        private void CanvasControl_OnDraw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            textEngine.SetDrawingSession(args.DrawingSession);

            if (layout == null)
            {
                return;
            }

            var width = Canvas.ActualWidth;
            var height = Canvas.ActualHeight;

            var availableSize = new Size(width, height);
            layout.Measure(availableSize);
            layout.Arrange(new Rect(Point.Zero, availableSize));

            layout.Render(new Win2DDrawingContext(args.DrawingSession));
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