using OmniXaml;

namespace OmniGui.Xaml
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using OmniXaml.Services;
    using Templates;

    public class OmniGuiXamlLoader : ExtendedXamlLoader
    {
        private readonly Func<ICollection<ControlTemplate>> getControlTemplates;
        private readonly ITypeLocator locator;

        public OmniGuiXamlLoader(IList<Assembly> assemblies, Func<ICollection<ControlTemplate>> getControlTemplates, ITypeLocator locator) : base(assemblies)
        {
            this.getControlTemplates = getControlTemplates;
            this.locator = locator;
            InstanceCreator = new OmniGuiInstanceCreator(locator);
        }

        public override IInstanceCreator InstanceCreator { get; } 
        protected override IValuePipeline ValuePipeline { get; } = new OmniGuiValuePipeline(new MarkupExtensionValuePipeline(new NoActionValuePipeline()));
    }
}