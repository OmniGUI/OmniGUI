namespace OmniGui.Xaml
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Layouts;
    using OmniXaml;
    using OmniXaml.TypeLocation;
    using Templates;

    public class OmniGuiInstanceCreator : IInstanceCreator
    {
        private readonly Func<ICollection<ControlTemplate>> getControlTemplates;
        private readonly InstanceCreator inner;

        public OmniGuiInstanceCreator(ISourceValueConverter sourceValueConverter, ObjectBuilderContext context, ITypeDirectory typeDirectory, Func<ICollection<ControlTemplate>> getControlTemplates)
        {
            this.getControlTemplates = getControlTemplates;
            inner = new InstanceCreator(sourceValueConverter, context, typeDirectory);
        }


        public object Create(Type type, BuildContext context, IEnumerable<InjectableMember> injectableMembers = null)
        {
            if (typeof(List).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
            {
                return new List(new TemplateInflator(), getControlTemplates);
            }

            return inner.Create(type, context, injectableMembers);
        }
    }
}