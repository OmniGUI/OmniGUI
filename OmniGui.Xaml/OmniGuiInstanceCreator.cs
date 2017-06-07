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

            return new CreationResult(Activator.CreateInstance(type), new CreationHints());
        }
    }
}