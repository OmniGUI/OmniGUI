namespace OmniGui
{
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
}