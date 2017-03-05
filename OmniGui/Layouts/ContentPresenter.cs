namespace OmniGui.Layouts
{
    using Zafiro.PropertySystem;

    public class ContentPresenter : ContentLayout
    {      
        public override void Render(IDrawingContext drawingContext)
        {
            base.Render(drawingContext);
        }

        public ContentPresenter(IPropertyEngine propertyEngine) : base(propertyEngine)
        {
        }
    }
}