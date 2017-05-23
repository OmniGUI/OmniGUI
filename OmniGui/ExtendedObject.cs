using OmniGui.OmniGui;

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
            return OmniGuiPlatform.PropertyEngine.GetProperty(propertyName, GetType());
        }
    }
}