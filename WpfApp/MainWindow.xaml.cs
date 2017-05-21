namespace WpfApp
{
    using System;
    using System.Collections.Generic;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Common;
    using Grace.DependencyInjection;
    using OmniGui;
    using OmniGui.Wpf;
    using OmniGui.Xaml;
    using OmniGui.Xaml.Templates;
    using OmniXaml;
    using OmniXaml.Attributes;
    using Zafiro.PropertySystem;
    using Point = OmniGui.Geometry.Point;
    using Rect = OmniGui.Geometry.Rect;
    using Size = OmniGui.Geometry.Size;

    public partial class MainWindow
    {
        private readonly Layout layout;

        public MainWindow()
        {
            InitializeComponent();

            Platform.Current = new WpfPlatform(this);
            
            var propertyEngine = new OmniGuiPropertyEngine();
            OmniGuiPlatform.PropertyEngine = propertyEngine;

            locator = new TypeLocator(() => ControlTemplates, propertyEngine);
            var xamlLoader = new OmniGuiXamlLoader(Assemblies.AssembliesInAppFolder.ToArray(), () => ControlTemplates, locator, propertyEngine);

            layout = (Layout) xamlLoader.Load(File.ReadAllText("Layout.xaml"));
            var container = (Container)xamlLoader.Load(File.ReadAllText("Container.xaml"));
            new TemplateInflator().Inflate(layout, container.ControlTemplates);
            ControlTemplates = container.ControlTemplates;
            layout.DataContext = new SampleViewModel(new WpfMessageBoxService());
        }

        public ICollection<ControlTemplate> ControlTemplates { get; }

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
        public static Func<string, ConvertContext, (bool, object)> BitmapConverter = (str, convertContext) => (true, GetBitmap(str));

        private ITypeLocator locator;

        private static Bitmap GetBitmap(string context)
        {
            var bitmap = new BitmapImage();
            
            using (var stream = File.OpenRead(context))
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

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Platform.Current.EventSource.Invalidate();
        }
    }
}