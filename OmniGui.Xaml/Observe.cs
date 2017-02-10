namespace OmniGui.Xaml
{
    using OmniXaml;
    public class Observe : IMarkupExtension
    {
        public Observe()
        {            
        }

        public Observe(string observableName)
        {
            ObservableName = observableName;
        }

        public object GetValue(ExtensionValueContext context)
        {
            return new ObserveDefinition(context.Assignment.Target.Instance, context.Assignment.Member, ObservableName, LinkMode);
        }

        public string ObservableName { get; set; }

        public LinkMode LinkMode { get; set; }

    }
}