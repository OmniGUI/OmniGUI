using System;

namespace OmniGui
{
    public struct CornerRadius : IEquatable<CornerRadius>
    {
        public CornerRadius(double uniform)
        {
            TopLeft = TopRight = BottomLeft = BottomRight = uniform;
        }

        public double TopLeft { get; set; }
        public double TopRight { get; set; }
        public double BottomLeft { get; set; }
        public double BottomRight { get; set; }

        public bool Equals(CornerRadius other)
        {
            return TopLeft.Equals(other.TopLeft) && TopRight.Equals(other.TopRight) && BottomLeft.Equals(other.BottomLeft) && BottomRight.Equals(other.BottomRight);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is CornerRadius && Equals((CornerRadius) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = TopLeft.GetHashCode();
                hashCode = (hashCode * 397) ^ TopRight.GetHashCode();
                hashCode = (hashCode * 397) ^ BottomLeft.GetHashCode();
                hashCode = (hashCode * 397) ^ BottomRight.GetHashCode();
                return hashCode;
            }
        }
    }
}