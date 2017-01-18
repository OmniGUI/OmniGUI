using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OmniGui;
using Point = OmniGui.Point;
using StackPanel = OmniGui.StackPanel;

namespace WpfApp
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {


        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var inner = new StackPanel()
            {                
                Background = OmniGui.Colors.Gray,
            };

            inner.AddChild(new StackPanel()
            {
                RequestedSize = new OmniGui.Size(80, 100),
                Background = OmniGui.Colors.Red,
            });

            inner.AddChild(new StackPanel()
            {
                RequestedSize = new OmniGui.Size(90, 100),
                Background = OmniGui.Colors.Green,
            });


            inner.AddChild(new StackPanel()
            {
                RequestedSize = new OmniGui.Size(100, 100),
                Background = OmniGui.Colors.Blue,
            });

            //var sp = new SimpleGrid()
            //    .AddChild(inner)
            //    .AddChild(new StackPanel
            //    {                    
            //        Background = OmniGui.Colors.Green
            //    })
            //    .AddChild(new StackPanel
            //    {                    
            //        Background = OmniGui.Colors.Blue
            //    });


            var width = ActualWidth;
            var height = ActualHeight;

            var availableSize = new OmniGui.Size(width, height);
            inner.Measure(availableSize);
            inner.Arrange(new OmniGui.Rect(Point.Zero, availableSize));

            inner.Render(new WpfContext(drawingContext));
        }
    }
}


