namespace OmniGui
{
    public interface IDrawingContext
    {
        void DrawRectangle(Rect rect, Brush brush, Pen pen);
        void DrawRoundedRectangle(Rect rect, Brush brush, Pen pen, CornerRadius cornerRadius);
        void DrawText(Point point, Brush brush, string text);
    }

    public class Pen
    {
        public Pen(Brush brush, double thickness)
        {
            Brush = brush;
            Thickness = thickness;
        }

        public Brush Brush { get; set; }
        public double Thickness { get; set; }
    }

    public class Brush
    {
        public Brush(Color color)
        {
            Color = color;
        }

        public Color Color { get; set; }
    }
}