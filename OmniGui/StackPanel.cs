using System;

namespace OmniGui
{
    public class StackPanel : Layout
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            double childAvailableWidth = double.PositiveInfinity;
            double childAvailableHeight = double.PositiveInfinity;

            if (Orientation == Orientation.Vertical)
            {
                childAvailableWidth = availableSize.Width;

                if (!double.IsNaN(Width))
                {
                    childAvailableWidth = Width;
                }

                childAvailableWidth = Math.Min(childAvailableWidth, MaxWidth);
                childAvailableWidth = Math.Max(childAvailableWidth, MinWidth);
            }
            else
            {
                childAvailableHeight = availableSize.Height;

                if (!double.IsNaN(Height))
                {
                    childAvailableHeight = Height;
                }

                childAvailableHeight = Math.Min(childAvailableHeight, MaxHeight);
                childAvailableHeight = Math.Max(childAvailableHeight, MinHeight);
            }

            double measuredWidth = 0;
            double measuredHeight = 0;
            double gap = Gap;

            foreach (var child in Children)
            {
                child.Measure(new Size(childAvailableWidth, childAvailableHeight));
                Size size = child.DesiredSize;

                if (Orientation == Orientation.Vertical)
                {
                    measuredHeight += size.Height + gap;
                    measuredWidth = Math.Max(measuredWidth, size.Width);
                }
                else
                {
                    measuredWidth += size.Width + gap;
                    measuredHeight = Math.Max(measuredHeight, size.Height);
                }
            }

            return new Size(measuredWidth, measuredHeight);
        }

        public double Gap { get; set; }

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
        public override void Render(IDrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(VisualBounds, Background, null);
            base.Render(drawingContext);
        }
    }
}