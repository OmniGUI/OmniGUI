namespace OmniGui.Tests
{
    public static class LayoutExtensions
    {
        public static Layout AddChild(this Layout layout, Layout child)
        {
            layout.Children.Add(child);
            return layout;
        }
    }
}