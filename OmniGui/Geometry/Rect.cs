namespace OmniGui.Geometry
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

        public bool Contains(Point point)
        {
            var isInsideHorz = point.X >= X && point.X <= X + Width;
            var isInsideVert = point.Y >= Y && point.Y <= Y + Height;

            return isInsideHorz && isInsideVert;
        }

        public Rect CenterIn(Rect rect)
        {
            return new Rect(X + (Width - rect.Width) / 2, Y + (Height - rect.Height) / 2, rect.Width, rect.Height);
        }

        /// <summary>
        /// Gets the right position of the rectangle.
        /// </summary>
        public double Right => X + Width;

        /// <summary>
        /// Gets the bottom position of the rectangle.
        /// </summary>
        public double Bottom => Y + Height;

        public Rect Intersect(Rect rect)
        {
            var newLeft = rect.X > X ? rect.X : X;
            var newTop = rect.Y > Y ? rect.Y : Y;
            var newRight = rect.Right < Right ? rect.Right : Right;
            var newBottom = rect.Bottom < Bottom ? rect.Bottom : Bottom;

            if (newRight > newLeft && newBottom > newTop)
            {
                return new Rect(newLeft, newTop, newRight - newLeft, newBottom - newTop);
            }

            return Empty;
        }

        public Rect Empty => default(Rect);
    }
}