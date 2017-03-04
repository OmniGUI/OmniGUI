namespace OmniGui.Xaml
{
    using System;
    using System.Collections.Generic;
    using Grace.DependencyInjection;
    using Templates;

    public class TypeResolver : ITypeResolver
    {
        private readonly DependencyInjectionContainer container;

        public TypeResolver(Func<ICollection<ControlTemplate>> getControlTemplates)
        {
            var container = new DependencyInjectionContainer();
            container.Configure(block =>
            {
                block.Export<TemplateInflator>().As<ITemplateInflator>().Lifestyle.Singleton();
                block.ExportInstance(() => getControlTemplates);                
            });
            
            
            this.container = container;
        }

        public bool TryResolve(Type type, out object instance)
        {
            try
            {
                return container.TryLocate(type, out instance);
            }
            catch (Exception e)
            {
                instance = null;
                return false;
            }
        }
    }
}