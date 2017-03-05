namespace OmniGui.Xaml
{
    using System;
    using System.Collections.Generic;
    using Grace.DependencyInjection;
    using Templates;
    using Zafiro.PropertySystem;

    public class TypeResolver : ITypeResolver
    {
        private readonly DependencyInjectionContainer container;

        public TypeResolver(Func<ICollection<ControlTemplate>> getControlTemplates)
        {
            var container = new DependencyInjectionContainer();
            container.Configure(block =>
            {
                block.ExportInstance(new PropertyEngine(GetParent)).As<IPropertyEngine>();
                block.Export<TemplateInflator>().As<ITemplateInflator>().Lifestyle.Singleton();
                block.ExportInstance(() => getControlTemplates);                
            });
            
            
            this.container = container;
        }

        private object GetParent(object o)
        {
            if (o is IChild)
            {
                return ((IChild) o).Parent;
            }

            return null;
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