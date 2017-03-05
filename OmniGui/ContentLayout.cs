namespace OmniGui
{
    using System;
    using OmniXaml.Attributes;
    using Zafiro.PropertySystem.Standard;
    using OmniGui.Layouts;
    using Zafiro.PropertySystem;

    public class ContentLayout : Layout
    {
        public static ExtendedProperty ContentProperty;

        public ContentLayout(IPropertyEngine propertyEngine) : base(propertyEngine)
        {
            RegistrationGuard.RegisterFor<ContentLayout>(() =>
            {
                ContentProperty = PropertyEngine.RegisterProperty("Content", typeof(ContentLayout), typeof(object), new PropertyMetadata { DefaultValue = null });
            });

            GetChangedObservable(ContentProperty).Subscribe(SetContent);            
        }

        private void SetContent(object o)
        {
            Children.Clear();
            if (o == null)
            {
                return;
            }

            var layout = o as Layout ?? new TextBlock(PropertyEngine) { Text = o.ToString()};
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