namespace OmniGui.Xaml
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using OmniXaml;
    using OmniXaml.Services;
    using OmniXaml.TypeLocation;
    using Templates;

    public class OmniGuiXamlLoader : XamlLoader
    {
        private readonly Func<ICollection<ControlTemplate>> getControlTemplates;

        public OmniGuiXamlLoader(IList<Assembly> assemblies, Func<ICollection<ControlTemplate>> getControlTemplates) : base(assemblies)
        {
            this.getControlTemplates = getControlTemplates;
        }

        protected override IObjectBuilder GetObjectBuilder(IInstanceCreator instanceCreator, ObjectBuilderContext context, ContextFactory factory)
        {
            return new OmniGuiXamlBuilder(instanceCreator, context, factory);
        }

        protected override IInstanceCreator GetInstanceCreator(ISourceValueConverter sourceValueConverter, ObjectBuilderContext context, ITypeDirectory typeDirectory)
        {
            return new OmniGuiInstanceCreator(sourceValueConverter, context, typeDirectory, getControlTemplates);
        }
    }
}