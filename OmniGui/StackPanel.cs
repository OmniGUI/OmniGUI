using System;

namespace OmniGui
{
    public class StackPanel : Layout
    {
        protected override Size MeasureOverride(Size availableSize)
        {
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

        protected override Size ArrangeOverride(Size finalSize)
        {
            var orientation = Orientation;
            double arrangedWidth = finalSize.Width;
            double arrangedHeight = finalSize.Height;

            if (Orientation == Orientation.Vertical)
            {
                arrangedHeight = 0;
            }
            else
            {
                arrangedWidth = 0;
            }

            foreach (var child in Children)
            {
                double childWidth = child.DesiredSize.Width;
                double childHeight = child.DesiredSize.Height;

                if (orientation == Orientation.Vertical)
                {
                    double width = Math.Max(childWidth, arrangedWidth);
                    var childFinal = new Rect(new Point(0, arrangedHeight), new Size(width, childHeight));
                    child.Arrange(childFinal);
                    arrangedWidth = Math.Max(arrangedWidth, childWidth);
                    arrangedHeight += childHeight;
                }
                else
                {
                    double height = Math.Max(childHeight, arrangedHeight);
                    var childFinal = new Rect(new Point(arrangedWidth, 0), new Size(childWidth, height));
                    child.Arrange(childFinal);
                    arrangedWidth += childWidth;
                    arrangedHeight = Math.Max(arrangedHeight, childHeight);
                }
            }

            if (orientation == Orientation.Vertical)
            {
                arrangedHeight = Math.Max(arrangedHeight, finalSize.Height);
            }
            else
            {
                arrangedWidth = Math.Max(arrangedWidth, finalSize.Width);
            }

            return new Size(arrangedWidth, arrangedHeight);
        }

        public Orientation Orientation { get; set; }
    }

    public enum Orientation
    {
        Vertical
    }
}