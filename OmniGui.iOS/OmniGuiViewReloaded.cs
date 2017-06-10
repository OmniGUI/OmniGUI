using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using CoreGraphics;
using OmniGui.Geometry;
using OmniGui.Xaml;
using OmniGui.Xaml.Templates;
using OmniXaml.Services;
using UIKit;

namespace OmniGui.iOS
{
    public class OmniGuiViewReloaded : UIView
    {
        private object dataContext;
        private string source;
        private Container container;

        public override void Draw(CGRect rect)
        {
            Layout.Measure(new Size(30, 30));
            Layout.Arrange(rect.ToOmniGui());
            using (var ctx = UIGraphics.GetCurrentContext())
            {
                Layout.Render(new iOSDrawingContext(ctx));
            }
        }

        public Layout Layout { get; set; }

        private IXamlLoader CreateXamlLoader(OmniGuiView view)
        {
            var androidEventSource = new iOSEventSource(view);
            var deps = new FrameworkDependencies(androidEventSource, new iOSRenderSurface(this), new iOSTextEngine());
            var typeLocator = new TypeLocator(() => ControlTemplates, deps);
            return new OmniGuiXamlLoader(Assemblies.AssembliesInAppFolder.ToArray(), () => ControlTemplates, typeLocator);
        }

        public string Source
        {
            get { return source; }
            set
            {
                source = value;
                SetSource(value);
            }
        }

        private void SetSource(string value)
        {
            try
            {
                var flacidLayout = (Layout)XamlLoader.Load(ReadMixin.ReadText(value));
                new TemplateInflator().Inflate(flacidLayout, ControlTemplates);
                Layout = flacidLayout;
                DataContext = DataContext;
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public ICollection<ControlTemplate> ControlTemplates => Container.ControlTemplates;

        public Container Container => container ?? (container = CreateContainer("Container.xaml"));

        private Container CreateContainer(string containerAsset)
        {
            return (Container)XamlLoader.Load(ReadMixin.ReadText(containerAsset));
        }

        public IXamlLoader XamlLoader { get; set; }

        public object DataContext
        {
            get { return dataContext; }
            set
            {
                dataContext = value;
                Layout.DataContext = value;
            }
        }
    }
}
