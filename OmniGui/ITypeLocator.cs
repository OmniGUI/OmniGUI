using System;

namespace OmniGui
{
    public interface ITypeLocator
    {
        bool TryLocate(Type type, out object instance);
    }
}