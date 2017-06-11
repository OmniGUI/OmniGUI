using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
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

        public OmniGuiViewReloaded()
        {
            XamlLoader = CreateXamlLoader(this);                       
        }

        public override void Draw(CGRect rect)
        {
            var bounds = rect.ToOmniGui();
            
            Layout.Measure(bounds.Size);
            Layout.Arrange(bounds);
            using (var ctx = UIGraphics.GetCurrentContext())
            {
                Layout.Render(new iOSDrawingContext(ctx));
            }
        }

        public Layout Layout { get; set; }

        private IXamlLoader CreateXamlLoader(UIView view)
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
                Layout.DataContext = DataContext;
            }
            catch (Exception e)
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

        public IXamlLoader XamlLoader { get; }

        public object DataContext
        {
            get { return dataContext; }
            set
            {
                dataContext = value;
                if (Layout != null)
                {
                    Layout.DataContext = value;
                }
            }
        }
    }
}
