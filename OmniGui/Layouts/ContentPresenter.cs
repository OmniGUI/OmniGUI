using System;
using System.Linq;

namespace OmniGui.Layouts
{
    public class ContentPresenter : ContentLayout
    {
        public ContentPresenter(FrameworkDependencies deps) : base(deps)
        {
            GetChangedObservable(ContentProperty).Subscribe(o =>
            {
                if (o == null)
                {
                    Children.Clear();
                }
                else
                {
                    var l = o as Layout;

                    Children.Add(l ?? new TextBlock(Deps) { Text = o.ToString() });
                }                
            });
        }
    }
}