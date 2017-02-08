namespace OmniGui.Xaml
{
    using OmniXaml;

    public class ObserveDefinition
    {
        public object TargetInstance { get; }
        public Member AssignmentMember { get; }
        public string ObservableName { get; }

        public ObserveDefinition(object targetInstance, Member assignmentMember, string observableName)
        {
            TargetInstance = targetInstance;
            AssignmentMember = assignmentMember;
            ObservableName = observableName;
        }
    }
}