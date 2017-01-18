using System;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;
using OmniGui;
using OmniGui.Console;
using OmniXaml.Services;
using Rect = OmniGui.Rect;

namespace WpfApp
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Layout layout;
        private ConsoleAdapter consoleAdapter;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //var xamlLoader = new XamlLoader(Assemblies.AssembliesInAppFolder.ToArray());

            //layout = (Layout)xamlLoader.Load(File.ReadAllText("Layout.xaml")).Instance;
            //consoleAdapter = new ConsoleAdapter();
            
            //Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(_ => UpdateConsole());
        }

        private void UpdateConsole()
        {
            var availableSize = new OmniGui.Size(Console.WindowWidth, Console.WindowHeight);

            layout.Measure(availableSize);
            layout.Arrange(Rect.FromZero(availableSize));
            layout.Render(consoleAdapter);
        }
    }
}
