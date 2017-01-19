namespace OmniGui
{
    public struct Rect
    {
        public Rect(Point point, Size size)
        {
            Point = point;
            Size = size;
        }

        public Rect(double originX, double originY, double sizeWidth, double sizeHeight)
            : this(new Point(originX, originY), new Size(sizeWidth, sizeHeight))
        {
        }

        public Point Point { get; }
        public Size Size { get; }
        public bool IsEmpty => Size.IsEmpty;
        public double X => Point.X;
        public double Y => Point.Y;
        public double Width => Size.Width;
        public double Height => Size.Height;

        public override string ToString()
        {
            return $"{nameof(Point)}: {Point}, {nameof(Size)}: {Size}";
        }

        public static Rect FromZero(Size size)
        {
            return new Rect(Point.Zero, size);
        }
    }
}