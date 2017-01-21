namespace OmniGui
{
    public interface IDrawingContext
    {
        void DrawRectangle(Rect rect, Color fillColor);
        void DrawRoundedRectangle(Rect rect, Color fillColor, Color borderColor, CornerRadius cornerRadius);
    }
}