using OmniGui.Geometry;

namespace OmniGui.Default
{
    public class DefaultDrawingContext : IDrawingContext
    {
        public void DrawRectangle(Pen pen, Rect rect)
        {
        }

        public void FillRectangle(Brush brush, Rect rect)
        {
        }

        public void DrawRoundedRectangle(Pen pen, Rect rect, CornerRadius cornerRadius)
        {
        }

        public void FillRoundedRectangle(Brush brush, Rect rect, CornerRadius cornerRadius)
        {            
        }

        public void DrawText(FormattedText formattedText, Point point, Rect? clipRect)
        {
        }

        public void DrawBitmap(Bitmap bmp, Rect sourceRect, Rect rect)
        {
        }

        public void DrawLine(Pen pen, Point startPoint, Point endPoint)
        {
        }

        public void PushClip(Rect rect)
        {            
        }

        public void Pop()
        {
            throw new System.NotImplementedException();
        }
    }
}