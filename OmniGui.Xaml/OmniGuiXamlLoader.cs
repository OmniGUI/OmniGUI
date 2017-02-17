namespace OmniGui.Xaml
{
    using System.Collections.Generic;
    using System.Reflection;
    using OmniXaml;
    using OmniXaml.Services;
    using OmniXaml.TypeLocation;

    public class OmniGuiXamlLoader : XamlLoader
    {
        public OmniGuiXamlLoader(IList<Assembly> assemblies) : base(assemblies)
        {            
        }

        protected override IObjectBuilder GetObjectBuilder(IInstanceCreator instanceCreator, ObjectBuilderContext context, ContextFactory factory)
        {
            return new OmniGuiXamlBuilder(instanceCreator, context, factory);
        }

        protected override IInstanceCreator GetInstanceCreator(ISourceValueConverter sourceValueConverter, ObjectBuilderContext context, ITypeDirectory typeDirectory)
        {
            return new OmniGuiInstanceCreator(sourceValueConverter, context, typeDirectory);
        }
    }
}