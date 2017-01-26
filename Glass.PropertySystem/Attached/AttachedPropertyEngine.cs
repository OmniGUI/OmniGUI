namespace Glass.PropertySystem.Attached
{
    using System;
    using System.Collections.Generic;

    public class AttachedPropertyEngine
    {
        private readonly Func<object, object> getParentFunc;

        private readonly MetadataStore<AttachedProperty, AttachedPropertyMetadata> metadataStore =
            new MetadataStore<AttachedProperty, AttachedPropertyMetadata>();

        private readonly IDictionary<AttachedPropertyEntry, object> values =
            new Dictionary<AttachedPropertyEntry, object>();

        public AttachedPropertyEngine(Func<object, object> getParentFunc)
        {
            this.getParentFunc = getParentFunc;
        }

        public void SetValue(AttachedProperty property, object instance, object value)
        {
            values.Add(new AttachedPropertyEntry(property, instance), value);
        }

        public AttachedProperty RegisterProperty(string name, Type ownerType, Type propertyType,
            AttachedPropertyMetadata propertyMetadata)
        {
            var prop = new AttachedProperty(ownerType, propertyType, name);
            metadataStore.RegisterMetadata(ownerType, prop, propertyMetadata);
            return prop;
        }

        public object GetValue(AttachedProperty property, object instance)
        {
            AttachedPropertyMetadata metadata;

            if (!metadataStore.TryGetMetadata(property.OwnerType, property, out metadata))
            {
                throw new InvalidOperationException();
            }

            if (metadata.Inherits)
            {
                object val;
                if (TryGetValueFromParentTree(property, instance, out val))
                {
                    return val;
                }
            }
            else
            {
                object val;
                var prop = new AttachedPropertyEntry(property, instance);
                if (values.TryGetValue(prop, out val))
                {
                    return val;
                }
            }

            return metadata.DefaultValue;
        }

        private bool TryGetValueFromParentTree(AttachedProperty property, object instance, out object val)
        {
            do
            {
                var prop = new AttachedPropertyEntry(property, instance);
                if (values.TryGetValue(prop, out val))
                {
                    return true;
                }

                instance = getParentFunc(instance);
            } while (instance != null);

            return false;
        }
    }
}