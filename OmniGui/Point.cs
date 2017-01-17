namespace OmniGui
{
    public struct Point
    {
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; }
        public double Y { get; }
        public static Point Zero => new Point(0, 0);


        public Point Offset(Point offset)
        {
            return new Point(offset.X + X, offset.Y + Y);
        }

        public override string ToString()
        {
            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}";
        }
    }
}