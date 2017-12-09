namespace OmniGui.VisualStates
{
    public class OldSetter
    {
        public void Apply()
        {         
            Target.Apply(Value);
        }

        public SetterTarget Target { get; set; }
        public object Value { get; set; }
    }
}