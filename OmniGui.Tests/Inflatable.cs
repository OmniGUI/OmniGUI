namespace OmniGui.Tests
{
    using Zafiro.PropertySystem.Standard;

    public class Inflatable : Layout
    {
        public ExtendedProperty TextProperty = PropertyEngine.RegisterProperty("Text", typeof(Inflatable), typeof(string), new PropertyMetadata());

        public Inflatable()
        {            
        }
    }
}