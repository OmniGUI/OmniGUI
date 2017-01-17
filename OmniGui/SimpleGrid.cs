namespace OmniGui
{
    public class SimpleGrid : Layout
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            return availableSize;
        }

        protected override Size ArrangeOverride(Rect rect)
        {
            return rect.Size;
        }
    }
}