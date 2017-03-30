namespace OmniGui.Xaml.Rework
{
    using System;
    using OmniXaml;
    using OmniXaml.Rework;

    public class OmniGuiInstanceCreator2 : ISmartInstanceCreator
    {
        private readonly ITypeLocator locator;

        public OmniGuiInstanceCreator2(IStringSourceValueConverter converter, ITypeLocator locator)
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

            throw new InvalidOperationException($"Cannot create instance ot type {type}");
        }
    }
}