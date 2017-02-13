namespace OmniGui
{
    using OmniXaml.Attributes;
    using Zafiro.PropertySystem.Standard;

    public class ContentLayout : Layout
    {
        public static readonly ExtendedProperty ContentProperty = PropertyEngine.RegisterProperty("Content", typeof(ContentLayout), typeof(object), new PropertyMetadata { DefaultValue = null });

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