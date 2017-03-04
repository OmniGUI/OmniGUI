namespace OmniGui.Xaml
{
    using System;

    public interface ITypeResolver
    {
        bool TryResolve(Type type, out object instance);
    }
}