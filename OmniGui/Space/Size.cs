namespace OmniGui.Space
{
    using System;

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

        /// <summary>
        /// Checks for equality between two <see cref="Size"/>s.
        /// </summary>
        /// <param name="left">The first size.</param>
        /// <param name="right">The second size.</param>
        /// <returns>True if the sizes are equal; otherwise false.</returns>
        public static bool operator ==(Size left, Size right)
        {
            return left.Width == right.Width && left.Height == right.Height;
        }

        /// <summary>
        /// Checks for unequality between two <see cref="Size"/>s.
        /// </summary>
        /// <param name="left">The first size.</param>
        /// <param name="right">The second size.</param>
        /// <returns>True if the sizes are unequal; otherwise false.</returns>
        public static bool operator !=(Size left, Size right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Scales a size.
        /// </summary>
        /// <param name="size">The size</param>
        /// <param name="scale">The scaling factor.</param>
        /// <returns>The scaled size.</returns>
        public static Size operator *(Size size, Vector scale)
        {
            return new Size(size.Width * scale.X, size.Height * scale.Y);
        }

        /// <summary>
        /// Scales a size.
        /// </summary>
        /// <param name="size">The size</param>
        /// <param name="scale">The scaling factor.</param>
        /// <returns>The scaled size.</returns>
        public static Size operator /(Size size, Vector scale)
        {
            return new Size(size.Width / scale.X, size.Height / scale.Y);
        }

        /// <summary>
        /// Scales a size.
        /// </summary>
        /// <param name="size">The size</param>
        /// <param name="scale">The scaling factor.</param>
        /// <returns>The scaled size.</returns>
        public static Size operator *(Size size, double scale)
        {
            return new Size(size.Width * scale, size.Height * scale);
        }

        /// <summary>
        /// Scales a size.
        /// </summary>
        /// <param name="size">The size</param>
        /// <param name="scale">The scaling factor.</param>
        /// <returns>The scaled size.</returns>
        public static Size operator /(Size size, double scale)
        {
            return new Size(size.Width / scale, size.Height / scale);
        }

        public static Size operator +(Size size, Size toAdd)
        {
            return new Size(size.Width + toAdd.Width, size.Height + toAdd.Height);
        }

        public static Size operator -(Size size, Size toSubstract)
        {
            return new Size(size.Width - toSubstract.Width, size.Height - toSubstract.Height);
        }
    }
}