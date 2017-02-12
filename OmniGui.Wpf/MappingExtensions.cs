using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace OmniGui.Wpf
{
    using Space;
    using FontWeight = System.Windows.FontWeight;
    using FontWeights = System.Windows.FontWeights;
    using Rect = Space.Rect;

    public static class MappingExtensions
    {
        public static System.Windows.Media.Color ToWpf(this Color color)
        {
            
            return System.Windows.Media.Color.FromArgb(color.Alpha, color.Red, color.Green, color.Blue);
        }

        public static System.Windows.Rect ToWpf(this Rect rect)
        {
            return new System.Windows.Rect(rect.Point.ToWpf(), rect.Size.ToWpf());
        }

        public static System.Windows.Point ToWpf(this Point point)
        {
            return new System.Windows.Point(point.X, point.Y);
        }

        public static System.Windows.Size ToWpf(this Size size)
        {
            return new System.Windows.Size(size.Width, size.Height);
        }

        public static System.Windows.Vector ToWpf(this Vector size)
        {
            return new System.Windows.Vector(size.X, size.Y);
        }

        public static SolidColorBrush ToWpf(this Brush brush)
        {
            if (brush == null)
            {
                return null;
            }

            return new System.Windows.Media.SolidColorBrush(brush.Color.ToWpf());
        }

        public static System.Windows.Media.FormattedText ToWpf(this FormattedText ft)
        {
            var weight = ft.FontWeight.ToWpf();
            
            var formattedText = new System.Windows.Media.FormattedText(ft.Text,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(new FontFamily("Arial"), new FontStyle(), weight, new FontStretch()), 15,
                ft.Brush.ToWpf(), new NumberSubstitution(), 3D)
            {                
                MaxTextWidth = double.IsInfinity(ft.Constraint.Width) ? 0 : ft.Constraint.Width,
                MaxTextHeight = ft.Constraint.Height,                
            };

            return formattedText;
        }

        public static FontWeight ToWpf(this OmniGui.FontWeights fontWeight)
        {
            if (fontWeight == OmniGui.FontWeights.Normal)
            {
                return FontWeights.Normal;
            }
            if (fontWeight == OmniGui.FontWeights.Bold)
            {
                return FontWeights.Bold;
            }
            if (fontWeight == OmniGui.FontWeights.ExtraBold)
            {
                return FontWeights.ExtraBold;
            }
            if (fontWeight == OmniGui.FontWeights.Light)
            {
                return FontWeights.Light;
            }

            return FontWeights.Normal;
        }

        public static System.Windows.Media.Pen ToWpf(this Pen pen)
        {
            if (pen == null)
            {
                return null;
            }

            return new System.Windows.Media.Pen(pen.Brush.ToWpf(), pen.Thickness);
        }
    }
}