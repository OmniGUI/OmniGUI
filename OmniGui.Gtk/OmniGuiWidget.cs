using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using Gdk;
using Gtk;
using Gtk.DotNet;
using OmniGui.Default;
using OmniXaml.Services;
using OmniGui.Xaml;
using OmniGui.Xaml.Templates;

namespace OmniGui.Gtk
{
    public class OmniGuiWidget : DrawingArea
    {
        private readonly IList<Assembly> assemblies;
        private string source;
        private ResourceStore resourceStore;
        private readonly GtkTextEngine textEngine;
        private object dataContext;

        public OmniGuiWidget(IList<Assembly> assemblies)
        {
            this.assemblies = assemblies;
            textEngine = new GtkTextEngine();
            XamlLoader = CreateXamlLoader();            
        }

        private IXamlLoader CreateXamlLoader()
        {
            var deps = new Platform(new GtkEventSource(this), new GtkRenderSurface(this), textEngine);
            var typeLocator = new TypeLocator(() => ResourceStore, deps, () => XamlLoader.StringSourceValueConverter);
            return new OmniGuiXamlLoader(assemblies, typeLocator, () => new StyleWatcher(ResourceStore.Styles));
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

        public ResourceStore ResourceStore => resourceStore ?? (resourceStore = CreateContainer("resourceStore.xaml"));

        private ResourceStore CreateContainer(string fileName)
        {
            return (ResourceStore)XamlLoader.Load(File.ReadAllText(fileName));
        }

        private void SetSource(string fileName)
        {
            var flacidLayout = (Layout)XamlLoader.Load(File.ReadAllText(fileName));
            new TemplateInflator().Inflate(flacidLayout, ControlTemplates);
            Layout = flacidLayout;
            Layout.DataContext = DataContext;
        }

        public ICollection<ControlTemplate> ControlTemplates => ResourceStore.ControlTemplates;

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


            var availableSize = size.ToOmniGui();
            

            using (var graphics = Graphics.FromDrawable(evnt.Window))
            {
                textEngine.Graphics = Graphics.FromDrawable(evnt.Window);
                Layout.Measure(availableSize);
                Layout.Arrange(new Geometry.Rect(Geometry.Point.Zero, availableSize));
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.CompositingMode =  CompositingMode.SourceOver;
                Layout.Render(new GtkDrawingContext(graphics));
            }
            
            return base.OnExposeEvent(evnt);
        }
    }
}