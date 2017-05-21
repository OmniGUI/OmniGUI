using System;
using System.Linq;
using System.Reflection;
using OmniXaml;
using Zafiro.Core;
using Zafiro.PropertySystem;

namespace OmniGui.Xaml
{
    public class OmniGuiInstanceCreator : IInstanceCreator
    {
        public CreationResult Create(Type type, CreationHints creationHints = null)
        {
            var needsPropertyEngine = type.GetTypeInfo().DeclaredConstructors.Any(info => info.GetParameters()
                .Any(pi => pi.ParameterType.IsAssignableFrom(typeof(IPropertyEngine))));

            if (needsPropertyEngine)
            {
                return new CreationResult(Activator.CreateInstance(type, OmniGuiPlatform.PropertyEngine), new CreationHints());
            }

            return new CreationResult(Activator.CreateInstance(type), new CreationHints());
        }
    }
}