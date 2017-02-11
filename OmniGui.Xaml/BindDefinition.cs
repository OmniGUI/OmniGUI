namespace OmniGui.Xaml
{
    using OmniXaml;

    public class BindDefinition
    {
        private readonly LinkMode linkMode;
        public object TargetInstance { get; }
        public Member TargetMember { get; }
        public string SourceProperty { get; }

        public BindDefinition(object targetInstance, Member targetMember, string sourceProperty, LinkMode linkMode)
        {
            this.linkMode = linkMode;
            TargetInstance = targetInstance;
            TargetMember = targetMember;
            SourceProperty = sourceProperty;
        }
        public LinkMode LinkMode { get; set; }
        public bool SourceFollowsTarget => linkMode == LinkMode.FullLink || linkMode == LinkMode.SourceFollowsTarget;
        public bool TargetFollowsSource => linkMode == LinkMode.FullLink || linkMode == LinkMode.TargetFollowsSource;
        public BindingSource Source { get; set; }
    }

    public enum BindingSource
    {
        DataContext,
        TemplatedParent,
    }
}