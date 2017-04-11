namespace OmniGui.VisualStates
{
    using OmniGui;
    using OmniXaml;

    public class SetterTarget
    {
        public object Target { get; }
        public Member Member { get; }

        public SetterTarget(object target, Member member)
        {
            Target = target;
            Member = member;
        }

        public void Apply(object value)
        {
            Member.SetValue(Target, value);
        }
    }
}