namespace OmniGui.Xaml
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using OmniXaml;
    using OmniXaml.Services;
    using Rework;
    using Templates;
    using IObjectBuilder = OmniXaml.Services.IObjectBuilder;
    using ObjectBuilder = OmniXaml.Services.ObjectBuilder;

    public class OmniGuiXamlLoader : XamlLoader
    {
        private readonly Func<ICollection<ControlTemplate>> getControlTemplates;
        private readonly ITypeLocator locator;

        public OmniGuiXamlLoader(IList<Assembly> assemblies, Func<ICollection<ControlTemplate>> getControlTemplates, ITypeLocator locator) : base(assemblies)
        {
            this.getControlTemplates = getControlTemplates;
            this.locator = locator;
        }

        protected override IObjectBuilder GetObjectBuilder(ISmartInstanceCreator instanceCreator, IStringSourceValueConverter converter)
        {
            return new ObjectBuilder(instanceCreator, converter);
        }

        protected override ISmartInstanceCreator GetInstanceCreator(IStringSourceValueConverter converter)
        {
            return new OmniGuiInstanceCreator2(converter, locator);
        }
    }
}