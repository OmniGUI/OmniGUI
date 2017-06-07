using Zafiro.PropertySystem.Standard;

namespace OmniGui.Xaml
{
    using System;
    using System.Collections.Generic;
    using Grace.DependencyInjection;
    using Serilog;
    using Templates;

    public class TypeLocator : ITypeLocator
    {
        private readonly DependencyInjectionContainer container;

        public TypeLocator(Func<ICollection<ControlTemplate>> getControlTemplates, FrameworkDependencies dependencies)
        {
            var injectionContainer = new DependencyInjectionContainer();
            injectionContainer.Configure(block =>
            {
                block.ExportFactory(() => dependencies);
                block.Export<TemplateInflator>().As<ITemplateInflator>().Lifestyle.Singleton();
                block.ExportFactory(() => getControlTemplates);                
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