namespace Glass.PropertySystem.Standard
{
    using System;
    using System.Collections.Generic;

    public class ExtendedPropertyEngine
    {
        private readonly MetadataStore<ExtendedProperty, PropertyMetadata> metadataStore =
            new MetadataStore<ExtendedProperty, PropertyMetadata>();

        private readonly IDictionary<PropertyEntry, object> values = new Dictionary<PropertyEntry, object>();

        public ExtendedProperty RegisterProperty(string name, Type ownerType, Type propertyType,
            PropertyMetadata metadata)
        {
            var prop = new ExtendedProperty(propertyType, name);
            metadataStore.RegisterMetadata(ownerType, prop, metadata);
            return prop;
        }

        public void SetValue(ExtendedProperty property, object instance, object value)
        {
            if (!metadataStore.Contains(instance.GetType(), property))
            {
                throw new InvalidOperationException();
            }

            values.Add(new PropertyEntry(property, instance), value);
        }

        public object GetValue(ExtendedProperty extendedProperty, object instance)
        {
            object retVal;
            if (values.TryGetValue(new PropertyEntry(extendedProperty, instance), out retVal))
            {
                return retVal;
            }

            PropertyMetadata metadata;
            if (metadataStore.TryGetMetadata(instance.GetType(), extendedProperty, out metadata))
            {
                return metadata.DefaultValue;
            }

            throw new InvalidOperationException();
        }
    }
}