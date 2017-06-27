using System.Linq;
using System.Reflection;
using Common;
using OmniGui.Gtk;
using Application = Gtk.Application;

namespace GtkApp
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var assemblies = typeof(MainClass).Assembly
                .GetReferencedAssemblies()
                .Concat(new[] { new AssemblyName("OmniGui.Xaml"), new AssemblyName("OmniGui"),  })
                .Select(Assembly.Load)
                .ToList();

            Application.Init();
            
            var window = new MainWindow();
            var omniGuiWidget = new OmniGuiWidget(assemblies) { Source = "layout.xaml", DataContext = new SampleViewModel(new GtkMessageService(window))};

            window.Add(omniGuiWidget);
            window.Show();
            omniGuiWidget.Show();
            Application.Run();
        }
    }
}