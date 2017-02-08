namespace OmniGui.Xaml
{
    using OmniXaml;

    public class BindDefinition
    {
        private readonly LinkMode linkMode;
        public object TargetInstance { get; }
        public Member AssignmentMember { get; }
        public string TargetProperty { get; }

        public BindDefinition(object targetInstance, Member assignmentMember, string targetProperty, LinkMode linkMode)
        {
            this.linkMode = linkMode;
            TargetInstance = targetInstance;
            AssignmentMember = assignmentMember;
            TargetProperty = targetProperty;
        }
        public LinkMode LinkMode { get; set; }
        public bool SourceFollowsTarget => linkMode == LinkMode.FullLink || linkMode == LinkMode.SourceFollowsTarget;
        public bool TargetFollowsSource => linkMode == LinkMode.FullLink || linkMode == LinkMode.TargetFollowsSource;
    }
}