using System;

namespace OmniGui.Layouts
{
    public class ContentPresenter : ContentLayout
    {
        public ContentPresenter(Platform platform) : base(platform)
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

                    Children.Add(l ?? new TextBlock(Platform) { Text = o.ToString() });
                }                
            });
        }
    }
}