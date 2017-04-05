namespace OmniGui
{
    using System;
    using OmniGui;
    using Zafiro.PropertySystem;
    using Zafiro.PropertySystem.Attached;
    using Zafiro.PropertySystem.Standard;

    public abstract class ExtendedObject : IPropertyHost
    {
        protected IPropertyEngine PropertyEngine { get; }

        public ExtendedObject(IPropertyEngine propertyEngine)
        {
            PropertyEngine = propertyEngine;
        }

        public void SetValue(AttachedProperty property, object value)
        {
            PropertyEngine.SetValue(property, this, value);
        }

        public void SetValue(ExtendedProperty property, object value)
        {
            PropertyEngine.SetValue(property, this, value);
        }

        public object GetValue(AttachedProperty property)
        {
            return PropertyEngine.GetValue(property, this);
        }

        public object GetValue(ExtendedProperty property)
        {
            return PropertyEngine.GetValue(property, this);
        }

        public IObservable<object> GetChangedObservable(ExtendedProperty property)
        {
            return PropertyEngine.GetChangedObservable(property, this);
        }

        public IObserver<object> GetObserver(ExtendedProperty property)
        {
            return PropertyEngine.GetObserver(property, this);
        }

        public ExtendedProperty GetProperty(string propertyName)
        {
            return PropertyEngine.GetProperty(propertyName, GetType());
        }
    }
}