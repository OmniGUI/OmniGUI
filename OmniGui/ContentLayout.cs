namespace OmniGui
{
    using System;
    using Layouts;
    using OmniXaml.Attributes;
    using Zafiro.PropertySystem.Standard;

    public class ContentLayout : Layout
    {
        public static readonly ExtendedProperty ContentProperty = OmniGuiPlatform.PropertyEngine.RegisterProperty("Content", typeof(ContentLayout), typeof(object), new PropertyMetadata { DefaultValue = null });

        public ContentLayout()
        {
            GetChangedObservable(ContentProperty).Subscribe(SetContent);            
        }

        private void SetContent(object o)
        {
            Children.Clear();
            if (o == null)
            {
                return;
            }

            var layout = o as Layout ?? new TextBlock() { Text = o.ToString()};
            Children.Add(layout);
        }

        public override void Render(IDrawingContext drawingContext)
        {
            base.Render(drawingContext);
        }

        [Content]
        public object Content
        {
            get { return GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
    }
}