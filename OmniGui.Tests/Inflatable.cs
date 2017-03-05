namespace OmniGui.Tests
{
    using Zafiro.PropertySystem;
    using Zafiro.PropertySystem.Standard;

    public class Inflatable : Layout
    {
        public ExtendedProperty TextProperty;

        public Inflatable(IPropertyEngine propertyEngine) : base(propertyEngine)
        {
            TextProperty = PropertyEngine.RegisterProperty("Text", typeof(Inflatable), typeof(string), new PropertyMetadata());
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
    }
}