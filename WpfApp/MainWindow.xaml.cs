namespace WpfApp
{
    using System.IO;
    using System.Linq;
    using System.Windows.Media;
    using Common;
    using OmniGui;
    using OmniGui.Tests;
    using OmniGui.Wpf;
    using OmniGui.Xaml;
    using Point = OmniGui.Point;
    using Rect = OmniGui.Rect;
    using Size = OmniGui.Size;

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
            var inflator = new Inflator();
            inflator.Inflate(layout, container.ControlTemplates);
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
    }
}