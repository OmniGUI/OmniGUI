using System;
using OmniXaml;

namespace OmniGui.Xaml
{
    public class OmniGuiInstanceCreator : IInstanceCreator
    {
        private readonly ITypeLocator locator;
        private readonly Func<StyleWatcher> styleWatcherSelector;

        public OmniGuiInstanceCreator(ITypeLocator locator, Func<StyleWatcher> styleWatcherSelector)
        {
            this.locator = locator;
            this.styleWatcherSelector = styleWatcherSelector;
        }

        public CreationResult Create(Type type, CreationHints creationHints = null)
        {
            if (locator.TryLocate(type, out var instance))
            {
                var layout = instance as Layout;
                if (layout != null)
                {
                    ObserveSelector(layout);
                }

                return new CreationResult(instance, new CreationHints());
            }

            try
            {
                var creationResult = new CreationResult(Activator.CreateInstance(type), new CreationHints());
                return creationResult;
            }
            catch (Exception e)
            {
                throw new TypeInitializationException($"Cannot create instance of type {type}: The dependency injection container could not create it and it doesn't have a default constructor. Please verify that all its dependencies have been registered with the resolver.", e);
            }            
        }

        private void ObserveSelector(Layout layout)
        {
            var styleWatcher = styleWatcherSelector();
            styleWatcher.Watch(layout);
        }
    }
}