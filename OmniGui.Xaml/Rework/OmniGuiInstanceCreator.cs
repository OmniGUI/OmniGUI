namespace OmniGui.Xaml.Rework
{
    using System;
    using OmniXaml;
    using OmniXaml.Rework;

    public class OmniGuiInstanceCreator : ISmartInstanceCreator
    {
        private readonly ITypeLocator locator;
        
        public OmniGuiInstanceCreator(IStringSourceValueConverter converter, ITypeLocator locator)
        {
            this.locator = locator;
        }

        public CreationResult Create(Type type, CreationHints creationHints = null)
        {
            object instance;
            var tryLocate = locator.TryLocate(type, out instance);

            if (tryLocate)
            {
                return new CreationResult(instance);
            }

            return new CreationResult(Activator.CreateInstance(type));           
        }
    }
}