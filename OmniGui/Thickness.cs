using System;
using System.Globalization;
using System.Linq;

namespace OmniGui
{
    public struct Thickness
    {
        public Thickness(double uniformLength)
        {
            Left = Top = Right = Bottom = uniformLength;
        }

        public double Left { get; set; }
        public double Top { get; set; }
        public double Right { get; set; }
        public double Bottom { get; set; }

        public static Thickness operator +(Thickness a, Thickness b)
        {
            return new Thickness(
                a.Left + b.Left,
                a.Top + b.Top,
                a.Right + b.Right,
                a.Bottom + b.Bottom);
        }

        public Thickness(double left, double top, double right, double bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        private Thickness(double left, double top)
        {
            Right = Left = left;
            Bottom = Top = top;
        }

        public static Thickness Parse(string s, CultureInfo culture)
        {
            var parts = s.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToList();

            switch (parts.Count)
            {
                case 1:
                    var uniform = double.Parse(parts[0], culture);
                    return new Thickness(uniform);
                case 2:
                    var horizontal = double.Parse(parts[0], culture);
                    var vertical = double.Parse(parts[1], culture);
                    return new Thickness(horizontal, vertical);
                case 4:
                    var left = double.Parse(parts[0], culture);
                    var top = double.Parse(parts[1], culture);
                    var right = double.Parse(parts[2], culture);
                    var bottom = double.Parse(parts[3], culture);
                    return new Thickness(left, top, right, bottom);
            }

            throw new FormatException("Invalid Thickness.");
        }
    }
}