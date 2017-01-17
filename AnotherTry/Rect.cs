namespace AnotherTry
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
    }
}