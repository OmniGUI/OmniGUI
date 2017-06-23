namespace OmniGui
{
    using Geometry;

    public interface IDrawingContext
    {
        void DrawRectangle(Pen pen, Rect rect);
        void FillRectangle(Brush brush, Rect rect);
        void DrawRoundedRectangle(Pen pen, Rect rect, CornerRadius cornerRadius);
        void FillRoundedRectangle(Brush brush, Rect rect, CornerRadius cornerRadius);
        void DrawText(FormattedText formattedText, Point point, Rect? clipRegion = null);
        void DrawBitmap(Bitmap bmp, Rect sourceRect, Rect rect);
        void DrawLine(Pen pen, Point startPoint, Point endPoint);
    }
}