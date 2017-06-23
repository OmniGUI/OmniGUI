using System.Linq;
using System.Reflection;
using Common;
using OmniGui;
using OmniGui.Gtk;
using Application = Gtk.Application;

namespace GtkApp
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            OmniGuiPlatform.PropertyEngine = new OmniGuiPropertyEngine();

            var assemblies = typeof(MainClass).Assembly
                .GetReferencedAssemblies()
                .Concat(new[] { new AssemblyName("OmniGui.Xaml"), new AssemblyName("OmniGui"),  })
                .Select(Assembly.Load)
                .ToList();

            Application.Init();
            //var win = new Window("System.Drawing and Gtk#");

            //var dw = new DrawingArea();
            //dw.ConfigureEvent += Dw_ConfigureEvent;
            //win.Add(dw);

            //dw.ExposeEvent += (o, a) => Dw_ExposeEvent(a);

            //dw.SetSizeRequest(300, 300);
            //win.Show();
            //dw.Show();
            var window = new MainWindow();
            var something = new OmniGuiWidget(assemblies) { Source = "layout.xaml", DataContext = new SampleViewModel(new GtkMessageService())};

            window.Add(something);
            window.Show();
            something.Show();
            Application.Run();
        }
    }
}