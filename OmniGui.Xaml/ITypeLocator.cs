namespace OmniGui.Xaml
{
    using System;

    public interface ITypeLocator
    {
        bool TryLocate(Type type, out object instance);
    }
}