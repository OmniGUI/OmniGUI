namespace OmniGui.VisualStates
{
    using System.Collections.ObjectModel;

    public class SetterCollection : Collection<Setter>
    {
        public SetterTarget Target { get; set; }
        public object Value { get; set; }
    }
}