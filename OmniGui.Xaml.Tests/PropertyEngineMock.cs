namespace OmniGui.Xaml.Tests
{
    using System;
    using Zafiro.PropertySystem;
    using Zafiro.PropertySystem.Attached;
    using Zafiro.PropertySystem.Standard;

    public class PropertyEngineMock : IPropertyEngine
    {
        public void SetValue(ExtendedProperty property, object instance, object value)
        {
            throw new NotImplementedException();
        }

        public void SetValue(AttachedProperty property, object instance, object value)
        {
            throw new NotImplementedException();
        }

        public object GetValue(ExtendedProperty property, object instance)
        {
            throw new NotImplementedException();
        }

        public object GetValue(AttachedProperty property, object instance)
        {
            throw new NotImplementedException();
        }

        public AttachedProperty RegisterProperty(string name, Type owner, Type propertyType, AttachedPropertyMetadata metadata)
        {
            throw new NotImplementedException();
        }

        public ExtendedProperty RegisterProperty(string name, Type owner, Type propertyType, PropertyMetadata metadata)
        {
            throw new NotImplementedException();
        }

        public ExtendedProperty GetProperty(string propertyName, Type type)
        {
            throw new NotImplementedException();
        }

        public IObservable<object> GetChangedObservable(ExtendedProperty property, object instance)
        {
            throw new NotImplementedException();
        }

        public IObserver<object> GetObserver(ExtendedProperty property, object instance)
        {
            throw new NotImplementedException();
        }
    }
}