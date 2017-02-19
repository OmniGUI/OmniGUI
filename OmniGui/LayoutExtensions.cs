namespace OmniGui
{
    using System.Linq;

    public static class LayoutExtensions
    {
        public static Layout AddChild(this Layout layout, Layout child)
        {
            layout.Children.Add(child);
            return layout;
        }

        public static T FindChild<T>(this Layout parent)
        {
            var first = parent.Children.OfType<T>().FirstOrDefault();
            if (first == null)
            {
                foreach (var child in parent.Children)
                {
                    var findChild = child.FindChild<T>();
                    if (findChild != null)
                    {
                        return findChild;
                    }
                }
            }

            return first;
        }
    }

    
}