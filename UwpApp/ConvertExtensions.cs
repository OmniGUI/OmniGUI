namespace UwpApp
{
    using Microsoft.Graphics.Canvas;
    using Microsoft.Graphics.Canvas.Brushes;
    using OmniGui;

    public static class ConvertExtensions
    {
        public static Windows.Foundation.Rect ToWin2D(this Rect rect)
        {
            return new Windows.Foundation.Rect(rect.Point.ToWin2D(), rect.Size.ToWin2D());
        }

        public static Windows.Foundation.Point ToWin2D(this Point rect)
        {
            return new Windows.Foundation.Point(rect.X, rect.Y);
        }

        public static Windows.Foundation.Point ToWin2D(this Size rect)
        {
            return new Windows.Foundation.Point(rect.Width, rect.Height);
        }

        public static Windows.UI.Color ToWin2D(this Color color)
        {
            return Windows.UI.Color.FromArgb(color.Alpha, color.Red, color.Green, color.Blue);
        }

        public static CanvasSolidColorBrush ToWin2D(this Brush brush, ICanvasResourceCreator resourceCreator)
        {
            return new CanvasSolidColorBrush(resourceCreator, brush.Color.ToWin2D());
        }
    }
}