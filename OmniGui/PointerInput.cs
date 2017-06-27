using OmniGui.Geometry;

namespace OmniGui
{
    public class PointerInput
    {
        public Point Point { get; set; }
        public PointerStatus PrimaryButtonStatus { get; set; }
        public PointerStatus SecondaryButtonStatus { get; set; }
    }
}