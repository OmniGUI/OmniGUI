using System.Linq;

namespace OmniGui
{
    public class SimpleGrid : Layout
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            return availableSize;
        }

        protected override Size ArrangeOverride(Size size)
        {
            var childWidth = size.Width / Children.Count();

            double x = 0;
            foreach (var layout in Children)
            {               
                layout.Arrange(new Rect(new Point(x, 0), new Size(childWidth, size.Height)));
                x += childWidth;
            }

            return size;
        }
    }
}