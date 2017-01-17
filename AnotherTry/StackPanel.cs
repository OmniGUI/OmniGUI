using System.Collections.Generic;
using System.Security.Cryptography;

namespace AnotherTry
{
    public class StackPanel
    {
        public Size RequestedSize { get; set; } = Size.Empty;
        public Size DesiredSize { get; set; }
        public IList<StackPanel> Children { get; set; } = new List<StackPanel>();

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
            double top = 0;
            foreach (var stackPanel in Children)
            {
                stackPanel.Arrange(new Rect(new Point(0, top), stackPanel.DesiredSize));
                top += stackPanel.DesiredSize.Height;
            }
        }

        private void Arrange(Rect finalSize)
        {
            this.Bounds = finalSize;
        }

        public Rect Bounds { get; set; }
    }
}