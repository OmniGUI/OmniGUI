namespace Glass.PropertySystem.Attached
{
    public class AttachedPropertyEntry
    {
        public AttachedProperty Property { get; }
        public object Instance { get; }

        public AttachedPropertyEntry(AttachedProperty property, object instance)
        {
            Property = property;
            Instance = instance;
        }

        protected bool Equals(AttachedPropertyEntry other)
        {
            return Property.Equals(other.Property) && Instance.Equals(other.Instance);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AttachedPropertyEntry) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Property.GetHashCode() * 397) ^ Instance.GetHashCode();
            }
        }
    }
}