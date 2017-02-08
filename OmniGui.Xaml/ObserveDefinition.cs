namespace OmniGui.Xaml
{
    using OmniXaml;

    public class ObserveDefinition
    {
        private readonly LinkMode linkMode;
        public object TargetInstance { get; }
        public Member AssignmentMember { get; }
        public string ObservableName { get; }

        public ObserveDefinition(object targetInstance, Member assignmentMember, string observableName, LinkMode linkMode)
        {
            this.linkMode = linkMode;
            TargetInstance = targetInstance;
            AssignmentMember = assignmentMember;
            ObservableName = observableName;
        }

        public bool SourceFollowsTarget => linkMode == LinkMode.FullLink || linkMode == LinkMode.SourceFollowsTarget;
        public bool TargetFollowsSource => linkMode == LinkMode.FullLink || linkMode == LinkMode.TargetFollowsSource;
    }
}