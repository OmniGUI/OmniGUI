namespace OmniGui
{
    public struct Rect
    {
        public Rect(Point point, Size size)
        {
            Point = point;
            Size = size;
        }

        public Point Point { get; }
        public Size Size { get; }

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