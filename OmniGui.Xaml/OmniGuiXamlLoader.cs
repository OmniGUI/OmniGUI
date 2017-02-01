namespace OmniGui.Xaml
{
    using System.Collections.Generic;
    using System.Reflection;
    using OmniXaml;
    using OmniXaml.Services;

    public class OmniGuiXamlLoader : XamlLoader
    {
        public OmniGuiXamlLoader(IList<Assembly> assemblies) : base(assemblies)
        {            
        }

        protected override IObjectBuilder GetObjectBuilder(InstanceCreator instanceCreator, ObjectBuilderContext context, ContextFactory factory)
        {
            return new OmniGuiXamlBuilder(instanceCreator, context, factory);
        }
    }
}