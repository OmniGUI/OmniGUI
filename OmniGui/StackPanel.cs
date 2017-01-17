namespace OmniGui
{
    public class StackPanel : IChild
    {
        public Color Background { get; set; } = Color.Transparent;
        public object Parent { get; set; }
        public Size RequestedSize { get; set; } = Size.Empty;
        public Size DesiredSize { get; set; }
        private OwnedList<StackPanel> Children { get; }

        public StackPanel()
        {
            Children = new OwnedList<StackPanel>(this);
        }

        public StackPanel AddChild(StackPanel child)
        {
            Children.Add(child);
            return this;
        }

        public void Measure(Size availableSize)
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

            DesiredSize = new Size(desiredX, desiredHeight);
        }

        public void Arrange(Size finalSize)
        {
            Arrange(new Rect(Point.Zero, finalSize));
        }

        private void Arrange(Rect finalSize)
        {
            double top = 0;
            foreach (var child in Children)
            {
                child.Arrange(new Rect(new Point(0, top), child.DesiredSize));
                top += child.DesiredSize.Height;
            }

            this.Bounds = finalSize;
        }

        public Rect Bounds { get; set; }

        public void Render(IDrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(VisualBounds, Background);
            foreach (var child in Children)
            {
                child.Render(drawingContext);
            }            
        }

        public Rect VisualBounds
        {
            get
            {
                if (Parent == null)
                {
                    return Bounds;
                }
                else
                {
                    var parent = (StackPanel) Parent;
                    var offset = parent.VisualBounds.Point.Offset(Bounds.Point);
                    return new Rect(offset, Bounds.Size);
                }
            }
        }
    }
}