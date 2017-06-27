using OmniXaml;

namespace OmniGui.Xaml
{
    using System;
    using Grace.DependencyInjection;
    using Serilog;

    public class TypeLocator : ITypeLocator
    {
        private readonly DependencyInjectionContainer container;

        public TypeLocator(Func<ResourceStore> containerFactory, Platform dependencies, Func<IStringSourceValueConverter> converter)
        {
            var injectionContainer = new DependencyInjectionContainer();
            injectionContainer.Configure(block =>
            {
                block.ExportFactory(converter).As<IStringSourceValueConverter>();
                block.ExportFactory(() => dependencies);
                block.ExportFactory(() => containerFactory);
            });
            
            container = injectionContainer;
        }

        public bool TryLocate(Type type, out object instance)
        {
            try
            {
                return container.TryLocate(type, out instance);
            }
            catch (Exception e)
            {
                Log.Error(e, "Cannot resolve {Type}", type);
                instance = null;
                return false;
            }
        }
    }
}