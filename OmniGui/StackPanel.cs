namespace OmniGui
{
    public class StackPanel : Layout
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            double desiredX = double.IsNaN(RequestedSize.Width) ? 0 : RequestedSize.Width;
            double desiredY = 0D;

            var sizeDesiredByChildren = 0D;
            foreach (var child in Children)
            {
                child.Measure(availableSize);
                sizeDesiredByChildren += child.DesiredSize.Height;
            }

            var desiredHeight = double.IsNaN(RequestedSize.Height)
                ? sizeDesiredByChildren
                : RequestedSize.Height;

            var desiredSize = new Size(desiredX, desiredHeight);
            return desiredSize;
        }

        protected override Size ArrangeOverride(Rect rect)
        {
            double top = 0;
            foreach (var child in Children)
            {
                child.Arrange(new Rect(new Point(0, top), child.DesiredSize));
                top += child.DesiredSize.Height;
            }

            Bounds = rect;

            return rect.Size;
        }
    }
}