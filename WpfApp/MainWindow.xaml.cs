namespace WpfApp
{
    using System;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Common;
    using OmniGui;
    using OmniGui.Wpf;
    using OmniGui.Xaml;
    using OmniXaml;
    using OmniXaml.Attributes;
    using Point = OmniGui.Space.Point;
    using Rect = OmniGui.Space.Rect;
    using Size = OmniGui.Space.Size;

    public partial class MainWindow
    {
        private readonly Layout layout;

        public MainWindow()
        {
            InitializeComponent();

            Platform.Current = new Platform
            {
                TextEngine = new WpfTextEngine(),
                EventDriver = new WpfEventProcessor(this)
            };

            var xamlLoader = new OmniGuiXamlLoader(Assemblies.AssembliesInAppFolder.ToArray());

            layout = (Layout) xamlLoader.Load(File.ReadAllText("Layout.xaml")).Instance;
            var container = (Container)xamlLoader.Load(File.ReadAllText("Container.xaml")).Instance;
            TemplateInflator.Inflate(layout, container.ControlTemplates);
            layout.DataContext = new SampleViewModel(new WpfMessageBoxService());
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var width = LayoutRoot.ActualWidth;
            var height = LayoutRoot.ActualHeight;

            var availableSize = new Size(width, height);
            layout.Measure(availableSize);
            layout.Arrange(new Rect(Point.Zero, availableSize));

            layout.Render(new WpfDrawingContext(drawingContext));
        }

        [TypeConverterMember(typeof(Bitmap))]
        public static Func<ConverterValueContext, object> BitmapConverter = context => GetBitmap(context);

        private static Bitmap GetBitmap(ConverterValueContext context)
        {
            var bitmap = new BitmapImage();
            
            using (var stream = File.OpenRead((string) context.Value))
            {
                bitmap.BeginInit();
                bitmap.StreamSource = stream;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
            }

            var frame = BitmapFrame.Create(bitmap);
            var buffer = new byte[frame.PixelWidth * frame.PixelHeight * 4];
            frame.CopyPixels(buffer, frame.PixelWidth * 4, 0);

            return new Bitmap
            {
                Height = frame.PixelHeight,
                Width = frame.PixelWidth,
                Bytes = buffer,
            };
        }
    }
}