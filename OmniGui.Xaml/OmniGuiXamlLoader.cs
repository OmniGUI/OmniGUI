namespace OmniGui.Xaml
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using OmniXaml;
    using OmniXaml.ReworkPhases;
    using OmniXaml.Services;
    using Rework;
    using Templates;
    using Zafiro.PropertySystem;
    using IObjectBuilder = OmniXaml.Services.IObjectBuilder;
    using ObjectBuilder = OmniXaml.Services.ObjectBuilder;

    public class OmniGuiXamlLoader : XamlLoader
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

        protected override IObjectBuilder GetObjectBuilder(ISmartInstanceCreator instanceCreator, IStringSourceValueConverter converter, IMemberAssigmentApplier memberAssigmentApplier)
        {
            return new ObjectBuilder(instanceCreator, converter, memberAssigmentApplier);
        }

        protected override ISmartInstanceCreator GetInstanceCreator(IStringSourceValueConverter converter)
        {
            return new Rework.OmniGuiInstanceCreator(converter, locator);
        }

        protected override IMemberAssigmentApplier GetMemberAssignmentApplier(IStringSourceValueConverter converter)
        {
            return new OmniGuiMemberAssignmentApplier(converter, new OmniGuiValuePipeline(new MarkupExtensionValuePipeline(new NoActionValuePipeline()), propertyEngine));
        }
    }
}