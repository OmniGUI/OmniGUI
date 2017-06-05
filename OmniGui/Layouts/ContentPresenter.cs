using System;
using System.Linq;

namespace OmniGui.Layouts
{
    public class ContentPresenter : ContentLayout
    {
        public ContentPresenter()
        {
            GetChangedObservable(ContentProperty).Subscribe(o =>
            {
                var l = o as Layout;
                Children.Add(l ?? new TextBlock() { Text = o.ToString() });
            });
        }
    }
}