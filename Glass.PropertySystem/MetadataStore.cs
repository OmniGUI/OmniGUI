namespace Glass.PropertySystem
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Core;

    public class MetadataStore<TProperty, TMetadata>
    {
        readonly IDictionary<Tuple<Type, TProperty>, TMetadata> store = new AutoKeyDictionary<Tuple<Type, TProperty>, TMetadata>(t => new Tuple<Type, TProperty>(t.Item1.GetTypeInfo().BaseType, t.Item2), t => t.Item1 != null);

        public void RegisterMetadata(Type type, TProperty property, TMetadata metadata)
        {
            var tuple = new Tuple<Type, TProperty>(type, property);
            store.Add(tuple, metadata);
        }

        public TMetadata GetMetadata(Type type, TProperty property)
        {
            var tuple = new Tuple<Type, TProperty>(type, property);
            return store[tuple];
        }

        public bool TryGetMetadata(Type type, TProperty property, out TMetadata metadata)
        {
            var tuple = new Tuple<Type, TProperty>(type, property);
            if (store.TryGetValue(tuple, out metadata))
            {
                return true;
            }

            return false;
        }

        public bool Contains(Type type, TProperty property)
        {
            return store.ContainsKey(new Tuple<Type, TProperty>(type, property));
        }
    }
}