namespace Glass.PropertySystem.Standard
{
    using System;

    public class ExtendedProperty
    {
        public Type PropertyType { get; }
        public string Name { get; }

        public ExtendedProperty(Type propertyType, string name)
        {
            this.PropertyType = propertyType;
            this.Name = name;
        }

        protected bool Equals(ExtendedProperty other)
        {
            return PropertyType == other.PropertyType && string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ExtendedProperty) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = PropertyType.GetHashCode();
                hashCode = (hashCode * 397) ^ Name.GetHashCode();
                return hashCode;
            }
        }
    }
}