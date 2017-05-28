using OmniXaml;

namespace OmniGui.Xaml
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using OmniXaml.Services;
    using Templates;
    using Zafiro.PropertySystem;

    public class OmniGuiXamlLoader : ExtendedXamlLoader
    {
        private readonly Func<ICollection<ControlTemplate>> getControlTemplates;
        private readonly ITypeLocator locator;
        private readonly IPropertyEngine propertyEngine;

        public OmniGuiXamlLoader(IList<Assembly> assemblies, Func<ICollection<ControlTemplate>> getControlTemplates, ITypeLocator locator, IPropertyEngine propertyEngine) : base(assemblies)
        {
            this.getControlTemplates = getControlTemplates;
            this.locator = locator;
            this.propertyEngine = propertyEngine;
        }

        public override IInstanceCreator InstanceCreator => new OmniGuiInstanceCreator();
        protected override IValuePipeline ValuePipeline => new OmniGuiValuePipeline(base.ValuePipeline);
    }
}