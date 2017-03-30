namespace OmniGui.Xaml
{
    using System;
    using System.Collections.Generic;
    using OmniXaml;
    using OmniXaml.TypeLocation;

    public class OmniGuiInstanceCreator : IInstanceCreator
    {
        private readonly ITypeLocator typeLocator;
        private readonly InstanceCreator inner;

        public OmniGuiInstanceCreator(ISourceValueConverter sourceValueConverter, ObjectBuilderContext context,
            ITypeDirectory typeDirectory, ITypeLocator typeLocator)
        {
            this.typeLocator = typeLocator;
            inner = new InstanceCreator(sourceValueConverter, context, typeDirectory);
        }

        public object Create(Type type, IBuildContext context, IEnumerable<InjectableMember> injectableMembers = null)
        {
            var instance = inner.Create(type, context, injectableMembers);
            if (instance == null)
            {
                if (typeLocator.TryLocate(type, out instance))
                {
                    return instance;
                }

                throw new Exception($"Cannot create instance of type {type}");
            }

            return instance;
        }
    }
}