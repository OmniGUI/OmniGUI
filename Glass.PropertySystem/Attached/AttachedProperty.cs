namespace Glass.PropertySystem.Attached
{
    using System;

    public class AttachedProperty
    {
        public Type OwnerType { get; }
        public Type PropertyType { get; }
        public string Name { get; }

        public AttachedProperty(Type ownerType, Type propertyType, string name)
        {
            OwnerType = ownerType;
            PropertyType = propertyType;
            Name = name;
        }

        protected bool Equals(AttachedProperty other)
        {
            return OwnerType == other.OwnerType && PropertyType == other.PropertyType && string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AttachedProperty) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = OwnerType.GetHashCode();
                hashCode = (hashCode * 397) ^ PropertyType.GetHashCode();
                hashCode = (hashCode * 397) ^ Name.GetHashCode();
                return hashCode;
            }
        }
    }
}