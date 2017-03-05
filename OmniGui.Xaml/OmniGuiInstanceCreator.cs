namespace OmniGui.Xaml
{
    using System;
    using System.Collections.Generic;
    using OmniXaml;
    using OmniXaml.TypeLocation;

    public class OmniGuiInstanceCreator : IInstanceCreator
    {
        private readonly ITypeResolver typeResolver;
        private readonly InstanceCreator inner;

        public OmniGuiInstanceCreator(ISourceValueConverter sourceValueConverter, ObjectBuilderContext context, ITypeDirectory typeDirectory, ITypeResolver typeResolver)
        {
            this.typeResolver = typeResolver;
            inner = new InstanceCreator(sourceValueConverter, context, typeDirectory);
        }


        public object Create(Type type, BuildContext context, IEnumerable<InjectableMember> injectableMembers = null)
        {
            try
            {
                var o = inner.Create(type, context, injectableMembers);
                return o;
            }
            catch (Exception e)
            {
                typeResolver.TryResolve(type, out var instance);

                if (instance == null)
                {
                    throw new Exception($"Cannot create instance of type {type}");
                }
                return instance;
            }         
        }
    }
}