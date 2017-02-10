namespace WpfApp
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using OmniGui;
    using OmniGui.Wpf;
    using OmniGui.Xaml;
    using OmniXaml;
    using OmniXaml.Attributes;
    using Point = OmniGui.Point;
    using Rect = OmniGui.Rect;
    using Size = OmniGui.Size;

    public partial class MainWindow
    {
        [TypeConverterMember(typeof(ICommand))] public static Func<ConverterValueContext, object> CommandConverter =
            context => new DelegateCommand(() => MessageBox.Show("Click!"));

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