namespace OmniGui.Xaml
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Layouts;
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

    public class OmniGuiInstanceCreator : IInstanceCreator
    {
        private readonly InstanceCreator inner;

        public OmniGuiInstanceCreator(ISourceValueConverter sourceValueConverter, ObjectBuilderContext context, ITypeDirectory typeDirectory)
        {
            inner = new InstanceCreator(sourceValueConverter, context, typeDirectory);
        }


        public object Create(Type type, BuildContext context, IEnumerable<InjectableMember> injectableMembers = null)
        {
            if (typeof(List).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
            {
                return new List(new TemplateInflator());
            }

            return inner.Create(type, context, injectableMembers);
        }
    }
}