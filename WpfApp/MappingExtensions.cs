using OmniGui;

namespace WpfApp
{
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
    }
}