namespace OmniGui
{
    public class Bitmap
    {
        public Bitmap(int width, int height, byte[] bytes)
        {
            Width = width;
            Height = height;
            Bytes = bytes;
        }

        public byte[] Bytes { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        protected bool Equals(Bitmap other)
        {
            return Bytes.Equals(other.Bytes) && Width == other.Width && Height == other.Height;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Bitmap) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Bytes.GetHashCode();
                hashCode = (hashCode * 397) ^ Width;
                hashCode = (hashCode * 397) ^ Height;
                return hashCode;
            }
        }
    }
}