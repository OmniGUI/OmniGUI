namespace OmniGui.Xaml
{
    using OmniXaml;

    public class BindDefinition
    {
        public object TargetInstance { get; }
        public Member AssignmentMember { get; }
        public string TargetProperty { get; }

        public BindDefinition(object targetInstance, Member assignmentMember, string targetProperty)
        {
            TargetInstance = targetInstance;
            AssignmentMember = assignmentMember;
            TargetProperty = targetProperty;
        }
    }
}