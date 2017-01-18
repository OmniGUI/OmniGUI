using System;

namespace OmniGui
{
    public class StackPanel : Layout
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            double desiredY = 0D;

            var childrenHeight = 0D;
            var childrenWidth = 0D;
            foreach (var child in Children)
            {
                child.Measure(availableSize);
                childrenHeight += child.DesiredSize.Height;
                childrenWidth = Math.Max(childrenWidth, child.DesiredSize.Width);
            }

            var desiredHeight = double.IsNaN(RequestedSize.Height)
                ? childrenHeight
                : RequestedSize.Height;

            var desiredWidth = double.IsNaN(RequestedSize.Width)
                ? childrenWidth
                : RequestedSize.Width;

            var desiredSize = new Size(desiredWidth, desiredHeight);
            return desiredSize;
        }

        protected override Size ArrangeOverride(Size size)
        {
            double top = 0;
            foreach (var child in Children)
            {
                child.Arrange(new Rect(new Point(0, top), child.DesiredSize));
                top += child.DesiredSize.Height;
            }

            Bounds = new Rect(Point.Zero, size);

            return DesiredSize;
        }
    }
}