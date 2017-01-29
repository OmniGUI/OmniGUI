using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using OmniGui;
using OmniGui.Wpf;
using OmniGui.Xaml;
using OmniXaml.Services;
using Point = OmniGui.Point;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        private readonly Layout layout;

        public MainWindow()
        {
            InitializeComponent();

            Platform.Current = new Platform();
            Platform.Current.TextEngine = new WpfTextEngine();
            Platform.Current.EventDriver = new WpfEventProcessor(this);

            var xamlLoader = new XamlLoader(Assemblies.AssembliesInAppFolder.ToArray());

            layout = (Layout) xamlLoader.Load(File.ReadAllText("Layout.xaml")).Instance;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var width = LayoutRoot.ActualWidth;
            var height = LayoutRoot.ActualHeight;

            var availableSize = new OmniGui.Size(width, height);
            layout.Measure(availableSize);
            layout.Arrange(new OmniGui.Rect(Point.Zero, availableSize));

            layout.Render(new WpfDrawingContext(drawingContext));
        }
    }
}