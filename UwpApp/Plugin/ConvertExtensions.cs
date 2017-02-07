using System;
using Windows.UI.Text;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using OmniGui;

namespace UwpApp.Plugin
{
    using FontWeights = OmniGui.FontWeights;

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

        public static Windows.UI.Text.FontWeight ToWin2D(this FontWeights fontWeight)
        {
            switch (fontWeight)
            {
                case FontWeights.Normal:
                    return Windows.UI.Text.FontWeights.Normal;
                case FontWeights.Bold:
                    return Windows.UI.Text.FontWeights.Bold;
                case FontWeights.ExtraBold:
                    return Windows.UI.Text.FontWeights.ExtraBold;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fontWeight), fontWeight, null);
            }
        }
    }
}