namespace OmniGui
{
    public struct Rect
    {
        public Rect(Point point, Size size)
        {
            Point = point;
            Size = size;
        }

        public Point Point { get; set; }
        public Size Size { get; set; }

        public override string ToString()
        {
            return $"{nameof(Point)}: {Point}, {nameof(Size)}: {Size}";
        }
    }
}