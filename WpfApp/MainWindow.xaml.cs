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
            var sp = new StackPanel()
                .AddChild(new StackPanel()
                {
                    RequestedSize = new OmniGui.Size(100, 100),
                    Background = OmniGui.Colors.Red,
                }.AddChild(new StackPanel()
                {
                    RequestedSize = new OmniGui.Size(50, 100),
                    Background = OmniGui.Color.FromArgb(128, 128, 128, 128)
                }))
                .AddChild(new StackPanel()
                {
                    RequestedSize = new OmniGui.Size(100, 100),
                    Background = OmniGui.Colors.Green
                })
                .AddChild(new StackPanel()
                {
                    RequestedSize = new OmniGui.Size(100, 100),
                    Background = OmniGui.Colors.Blue
                });


            sp.Measure(new OmniGui.Size(1000, 1000));
            sp.Arrange(new OmniGui.Size(1000, 1000));

            sp.Render(new WpfContext(drawingContext));
        }
    }
}


