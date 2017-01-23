using System;

namespace OmniGui
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
        public static Size Unspecified => new Size(double.NaN, double.NaN);
        public static Size Zero => new Size(0, 0);
        public static Size NoneSpecified => new Size(double.NaN, double.NaN);
        public bool IsEmpty => Width == 0 || Height == 0;
        public static Size Empty => Zero;
        public static Size Infinite => new Size(double.PositiveInfinity, double.PositiveInfinity);
        
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

        public Size WithWidth(double width)
        {
            return new Size(width, Height);
        }

        public Size WithHeight(double height)
        {
            return new Size(Width, height);
        }

        public Size Constrain(Size constraint)
        {
            return new Size(
                Math.Min(Width, constraint.Width),
                Math.Min(Height, constraint.Height));
        }

        public Size Deflate(Thickness thickness)
        {
            return new Size(
                Math.Max(0, Width - thickness.Left - thickness.Right),
                Math.Max(0, Height - thickness.Top - thickness.Bottom));
        }

        public Size Inflate(Thickness thickness)
        {
            return new Size(
                Width + thickness.Left + thickness.Right,
                Height + thickness.Top + thickness.Bottom);
        }
    }
}