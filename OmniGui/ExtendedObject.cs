using System.Reflection;

namespace OmniGui
{
    using System;
    using Zafiro.PropertySystem.Attached;
    using Zafiro.PropertySystem.Standard;

    public abstract class ExtendedObject : IPropertyHost
    {

        public void SetValue(AttachedProperty property, object value)
        {
            OmniGuiPlatform.PropertyEngine.SetValue(property, this, value);
        }

        public void SetValue(ExtendedProperty property, object value)
        {
            OmniGuiPlatform.PropertyEngine.SetValue(property, this, value);
        }

        public object GetValue(AttachedProperty property)
        {
            return OmniGuiPlatform.PropertyEngine.GetValue(property, this);
        }

        public object GetValue(ExtendedProperty property)
        {
            return OmniGuiPlatform.PropertyEngine.GetValue(property, this);
        }

        public IObservable<object> GetChangedObservable(ExtendedProperty property)
        {
            return OmniGuiPlatform.PropertyEngine.GetChangedObservable(property, this);
        }

        public IObserver<object> GetObserver(ExtendedProperty property)
        {
            return OmniGuiPlatform.PropertyEngine.GetObserver(property, this);
        }

        public ExtendedProperty GetProperty(string propertyName)
        {
            var runtimeField = GetFieldRecursive(this.GetType(), propertyName + "Property");
            return (ExtendedProperty) runtimeField.GetValue(null);
        }

        private FieldInfo GetFieldRecursive(Type type, string name)
        {
            if (type == null)
            {
                throw new InvalidOperationException("Attempt to get a property from a null type");
            }

            var field = type.GetRuntimeField(name);
            if (field == null)
            {
                return GetFieldRecursive(type.GetTypeInfo().BaseType, name);
            }

            return field;
        }
    }
}