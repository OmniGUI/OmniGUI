namespace OmniGui
{
    public interface IDrawingContext
    {
        void DrawRectangle(Rect rect, Color fillColor);
        void DrawRectangle(Rect rect, Color fillColor, Color borderColor);
    }
}