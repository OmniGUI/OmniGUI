using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Gdk;
using Gtk;
using Gtk.DotNet;
using OmniGui.Default;
using OmniGui.Uwp;
using OmniXaml.Services;
using OmniGui.Xaml;
using OmniGui.Xaml.Templates;
using Container = OmniGui.Xaml.Container;

namespace OmniGui.Gtk
{
    public class OmniGuiWidget : DrawingArea
    {
        private readonly IList<Assembly> assemblies;
        private string source;
        private Container container;
        private readonly GtkTextEngine gtkTextEngine;
        private object dataContext;

        public OmniGuiWidget(IList<Assembly> assemblies)
        {
            this.assemblies = assemblies;
            gtkTextEngine = new GtkTextEngine();
            XamlLoader = CreateXamlLoader();            
        }

        private IXamlLoader CreateXamlLoader()
        {
            var deps = new FrameworkDependencies(new DefaultEventSource(), new DefaultRenderSurface(), gtkTextEngine);
            var typeLocator = new TypeLocator(() => ControlTemplates, deps);
            return new OmniGuiXamlLoader(assemblies, () => ControlTemplates, typeLocator);
        }

        public IXamlLoader XamlLoader { get; }

        public string Source
        {
            get => source;
            set
            {
                source = value;
                SetSource(value);
            }
        }

        public Container Container => container ?? (container = CreateContainer("container.xaml"));

        private Container CreateContainer(string fileName)
        {
            return (Container)XamlLoader.Load(File.ReadAllText(fileName));
        }

        private void SetSource(string fileName)
        {
            var flacidLayout = (Layout)XamlLoader.Load(File.ReadAllText(fileName));
            new TemplateInflator().Inflate(flacidLayout, ControlTemplates);
            Layout = flacidLayout;
            Layout.DataContext = DataContext;
        }

        public ICollection<ControlTemplate> ControlTemplates => Container.ControlTemplates;

        public Layout Layout { get; set; }

        public object DataContext
        {
            get { return dataContext; }
            set
            {
                dataContext = value;
                Layout.DataContext = value;
            }
        }

        protected override bool OnExposeEvent(EventExpose evnt)
        {
            var size = Allocation.Size;

            gtkTextEngine.Graphics = Graphics.FromDrawable(evnt.Window);

            var context = new GtkDrawingContext(evnt);
            var availableSize = size.ToOmniGui();
            Layout.Measure(availableSize);
            Layout.Arrange(new Geometry.Rect(Geometry.Point.Zero, availableSize));
            Layout.Render(context);

            return base.OnExposeEvent(evnt);
        }
    }
}