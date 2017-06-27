using System;
using OmniXaml;

namespace OmniGui.Xaml
{
    public class OmniGuiInstanceCreator : IInstanceCreator
    {
        private readonly ITypeLocator locator;

        public OmniGuiInstanceCreator(ITypeLocator locator)
        {
            this.locator = locator;
        }

        public CreationResult Create(Type type, CreationHints creationHints = null)
        {
            if (locator.TryLocate(type, out var result))
            {
                return new CreationResult(result, new CreationHints());
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
    }
}