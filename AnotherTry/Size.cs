namespace AnotherTry
{
    public struct Size
    {
        public Size(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public double Width { get; set; }
        public double Height { get; set; }
        public static Size Empty => new Size(double.NaN, double.NaN);
        public static Size Zero => new Size(0, 0);
        public static Size NoneSpecified => new Size(double.NaN, double.NaN);

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }

        public static Size OnlyHeight(double height)
        {
            return new Size(double.NaN, height);
        }

        public static Size OnlyWidth(double width)
        {
            return new Size(width, double.NaN);
        }

        public static Size HeightAndZero(double height)
        {
            return new Size(0, height);
        }

        public static Size WidthAndZero(double width)
        {
            return new Size(width, 0);
        }
    }
}