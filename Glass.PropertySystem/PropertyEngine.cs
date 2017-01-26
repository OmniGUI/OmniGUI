using System;
using Glass.PropertySystem.Attached;
using Glass.PropertySystem.Standard;

namespace Glass.PropertySystem
{
    public class PropertyEngine
    {
        private readonly AttachedPropertyEngine attachedEngine;
        private readonly ExtendedPropertyEngine stdEngine;

        public PropertyEngine(Func<object, object> getParentFunc)
        {
            attachedEngine = new AttachedPropertyEngine(getParentFunc);
            stdEngine = new ExtendedPropertyEngine();
        }

        public void SetValue(ExtendedProperty property, object instance, object value)
        {
            stdEngine.SetValue(property, instance, value);
        }

        public void SetValue(AttachedProperty property, object instance, object value)
        {
            attachedEngine.SetValue(property, instance, value);
        }

        public object GetValue(ExtendedProperty property, object instance)
        {
            return stdEngine.GetValue(property, instance);
        }

        public object GetValue(AttachedProperty property, object instance)
        {
            return attachedEngine.GetValue(property, instance);
        }

        public AttachedProperty RegisterProperty(string name, Type owner, Type propertyType, AttachedPropertyMetadata metadata)
        {
            return attachedEngine.RegisterProperty(name, owner, propertyType, metadata);
        }

        public ExtendedProperty RegisterProperty(string name, Type owner, Type propertyType, PropertyMetadata metadata)
        {
            return stdEngine.RegisterProperty(name, propertyType, owner, metadata);
        }
    }
}