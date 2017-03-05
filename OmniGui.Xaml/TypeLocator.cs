namespace OmniGui.Xaml
{
    using System;
    using System.Collections.Generic;
    using Grace.DependencyInjection;
    using Serilog;
    using Templates;
    using Zafiro.PropertySystem;

    public class TypeLocator : ITypeLocator
    {
        private readonly DependencyInjectionContainer container;

        public TypeLocator(Func<ICollection<ControlTemplate>> getControlTemplates)
        {
            var injectionContainer = new DependencyInjectionContainer();
            injectionContainer.Configure(block =>
            {
                block.ExportInstance(new PropertyEngine(GetParent)).As<IPropertyEngine>();
                block.Export<TemplateInflator>().As<ITemplateInflator>().Lifestyle.Singleton();
                block.ExportInstance(() => getControlTemplates);                
            });
            
            
            container = injectionContainer;
        }

        private static object GetParent(object o)
        {
            var child = o as IChild;
            return child?.Parent;
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