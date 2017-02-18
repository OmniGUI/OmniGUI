namespace UwpApp
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Windows.Storage;
    using Windows.Storage.Streams;
    using Windows.UI.Popups;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Common;
    using Microsoft.Graphics.Canvas.UI.Xaml;
    using OmniGui;
    using OmniGui.Geometry;
    using OmniGui.Xaml;
    using OmniXaml;
    using OmniXaml.Attributes;
    using Plugin;
    using BitmapDecoder = Windows.Graphics.Imaging.BitmapDecoder;
    using ControlTemplate = OmniGui.Xaml.Templates.ControlTemplate;

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
                Assembly.Load(new AssemblyName("Common"))
            }, () => ControlTemplates);

            layout = (Layout) xamlLoader.Load(await ReadAllText("Layout.xaml")).Instance;
            var container = (Container)xamlLoader.Load(await ReadAllText("Container.xaml")).Instance;
            var inflator = new TemplateInflator();
            inflator.Inflate(layout, container.ControlTemplates);
            ControlTemplates = container.ControlTemplates;
            layout.DataContext = new SampleViewModel(new UwpMessageService());
        }

        public ICollection<ControlTemplate> ControlTemplates { get; set; }


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

        private static async Task<string> ReadAllText(string fileName)
        {
            var uri = new Uri($"ms-appx:///{fileName}");
            var file = await StorageFile.GetFileFromApplicationUriAsync(uri);
            var xaml = await FileIO.ReadTextAsync(file);
            return xaml;
        }

        [TypeConverterMember(typeof(Bitmap))]
        public static Func<ConverterValueContext, object> BitmapConverter = context => GetBitmap(context).Result;

        private static Bitmap GetResult(ConverterValueContext context)
        {
            return GetBitmap(context)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }

        private static async Task<Bitmap> GetBitmap(ConverterValueContext context)
        {
            var stream = await GetStream((string) context.Value).ConfigureAwait(false);
            using (stream)
            {
                return await CreateFromStream(stream);
            }           
        }

        private static async Task<Bitmap> CreateFromStream(IRandomAccessStream stream)
        {
            var decoder = await BitmapDecoder.CreateAsync(BitmapDecoder.PngDecoderId, stream);
            var frame = await decoder.GetFrameAsync(0);
            var pixelData = await frame.GetPixelDataAsync();
            var bytes = pixelData.DetachPixelData();

            return new Bitmap
            {
                Bytes = bytes,
                Width = (int) frame.PixelWidth,
                Height = (int) frame.PixelHeight,
            };
        }

        private static async Task<IRandomAccessStream> GetStream(string fileName)
        {
            var uri = new Uri($"ms-appx:///{fileName}");
            var storageFile = await StorageFile.GetFileFromApplicationUriAsync(uri).AsTask().ConfigureAwait(false);
            return await storageFile.OpenAsync(FileAccessMode.Read).AsTask().ConfigureAwait(false);
        }
    }
}