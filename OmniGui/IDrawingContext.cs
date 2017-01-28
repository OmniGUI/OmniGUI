namespace OmniGui
{
    public interface IDrawingContext
    {
        void DrawRectangle(Rect rect, Brush brush, Pen pen);
        void DrawRoundedRectangle(Rect rect, Brush brush, Pen pen, CornerRadius cornerRadius);
        void DrawText(Point point, Brush brush, string text);

        void DrawText(FormattedText formattedText, Point point);
    }
}