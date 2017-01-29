namespace UwpApp
{
    using System;
    using Windows.UI.Text;
    using Microsoft.Graphics.Canvas;
    using Microsoft.Graphics.Canvas.Brushes;
    using OmniGui;
    using FontWeight = OmniGui.FontWeight;

    public static class ConvertExtensions
    {
        public static Windows.Foundation.Rect ToWin2D(this Rect rect)
        {
            return new Windows.Foundation.Rect(rect.X, rect.Y, rect.Width, rect.Height);
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

        public static Windows.UI.Text.FontWeight ToWin2D(this FontWeight fontWeight)
        {
            switch (fontWeight)
            {
                case FontWeight.Normal:
                    return FontWeights.Normal;
                case FontWeight.Bold:
                    return FontWeights.Bold;
                case FontWeight.ExtraBold:
                    return FontWeights.ExtraBold;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fontWeight), fontWeight, null);
            }
        }
    }
}