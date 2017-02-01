using Android.Graphics;
using OmniGui;
using Rect = Android.Graphics.Rect;
using Size = OmniGui.Size;

namespace AndroidApp.AndPlugin
{
    public class AndroidTextEngine : ITextEngine
    {
        public Size Measure(FormattedText formattedText)
        {
            var paint = new Paint();
            var rect = new Rect();
            paint.GetTextBounds(formattedText.Text, 0, formattedText.Text.Length, rect);
            
            
            return new Size(rect.Width(), rect.Height());
        }       
    }
}