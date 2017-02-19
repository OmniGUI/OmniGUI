namespace OmniGui.Geometry
{
    internal struct Segment
    {
        public Point P1 { get; }
        public Point P2 { get; }

        public Segment(Point p1, Point p2)
        {
            P1 = p1;
            P2 = p2;
        }
    }
}