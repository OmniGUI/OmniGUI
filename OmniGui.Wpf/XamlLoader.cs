using System;
using System.Collections.Generic;
using System.Linq;
using OmniGui.Xaml;
using OmniGui.Xaml.Templates;
using OmniXaml.Services;
using WpfApp;

namespace OmniGui.Wpf
{
    public class XamlLoader
    {
        private static IXamlLoader loader;
        private static ITemplateInflator inflator;
        private static Container container;

        public static Layout Load(string xaml)
        {
            var layout = (Layout)Loader.Load(xaml);
            Inflator.Inflate(layout, Container.ControlTemplates);
            return layout;
        }

        public static ITemplateInflator Inflator => inflator ?? (inflator = CreateInflator());

        private static ITemplateInflator CreateInflator()
        {
            return new TemplateInflator();
        }

        public static IXamlLoader Loader => loader ?? (loader = CreateLoader());

        private static IXamlLoader CreateLoader()
        {
            var assemblies = Assemblies.AssembliesInAppFolder.ToArray();
            var l = new OmniGuiXamlLoader(assemblies, () => ControlTemplates, new TypeLocator(() => ControlTemplates));
            return l;
        }

        public static ICollection<ControlTemplate> ControlTemplates => Container.ControlTemplates;

        public static Container Container
        {
            get { return container ?? (container = CreateContainer(new Uri("Container.xaml", UriKind.RelativeOrAbsolute))); }
        }

        private static Container CreateContainer(Uri containerXaml)
        {
            return (Container)Loader.Load(containerXaml.ReadFromContent());
        }
    }
}