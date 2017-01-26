namespace Glass.PropertySystem.Standard
{
    internal class PropertyEntry
    {
        public PropertyEntry(ExtendedProperty property, object instance)
        {
            Property = property;
            Instance = instance;
        }

        public object Instance { get; set; }
        public ExtendedProperty Property { get; set; }

        protected bool Equals(PropertyEntry other)
        {
            return Instance.Equals(other.Instance) && Property.Equals(other.Property);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((PropertyEntry) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Instance.GetHashCode() * 397) ^ Property.GetHashCode();
            }
        }
    }
}