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

        public Rect(Size size) : this(Point.Zero, size)
        {            
        }

        public Point Point { get; }
        public Size Size { get; }
        public bool IsEmpty => Size.IsEmpty;
        public double X => Point.X;
        public double Y => Point.Y;
        public double Width => Size.Width;
        public double Height => Size.Height;
        public Point TopLeft => Point;
        public Point TopRight => new Point(Point.X + Width, Y);
        public Point BottomLeft => new Point(X, Y + Height);
        public Point BottomRight => new Point(X + Width, Y + Height);

        public override string ToString()
        {
            return $"{nameof(Point)}: {Point}, {nameof(Size)}: {Size}";
        }

        public static Rect FromZero(Size size)
        {
            return new Rect(Point.Zero, size);
        }

        public Rect Deflate(double thickness)
        {
            return Deflate(new Thickness(thickness / 2));
        }

        /// <summary>
        /// Deflates the rectangle by a <see cref="Thickness"/>.
        /// </summary>
        /// <param name="thickness">The thickness.</param>
        /// <returns>The deflated rectangle.</returns>
        /// <remarks>The deflated rectangle size cannot be less than 0.</remarks>
        public Rect Deflate(Thickness thickness)
        {
            return new Rect(
                new Point(X + thickness.Left, Y + thickness.Top),
                Size.Deflate(thickness));
        }
    }

}