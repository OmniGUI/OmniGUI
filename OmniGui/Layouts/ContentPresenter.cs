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
                Children.Add(new TextBlock() { Text = o.ToString() });
            });
        }
    }
}