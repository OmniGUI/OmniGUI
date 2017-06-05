using Android.Graphics;
using OmniGui;

namespace AndroidApp.AndPlugin
{
    using OmniGui.Geometry;
    using Rect = Rect;

    public class AndroidTextEngine : ITextEngine
    {
        public Size Measure(FormattedText formattedText)
        {
            var paint = new Paint();
            var rect = new Rect();
            paint.TextSize = formattedText.FontSize;          
            paint.GetTextBounds(formattedText.Text, 0, formattedText.Text.Length, rect);

           
            return new Size(rect.Width(), rect.Height());
        }

        public double GetHeight(string fontFamily, float fontSize)
        {
            var paint = new Paint();
            paint.TextSize = fontSize;

            var fontMetrics = paint.GetFontMetrics(null);

            return fontMetrics;
        }
    }
}