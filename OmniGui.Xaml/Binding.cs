namespace OmniGui.Xaml
{
    using OmniXaml;
    public class Binding : IMarkupExtension
    {
        public Binding()
        {            
        }

        public Binding(string observableName)
        {
            ObservableName = observableName;
        }

        public object GetValue(ExtensionValueContext context)
        {
            return new BindingDefinition(context.Assignment.Target.Instance, context.Assignment.Member, ObservableName);
        }

        public string ObservableName { get; set; }
    }

    public class BindingDefinition
    {
        public object TargetInstance { get; }
        public Member AssignmentMember { get; }
        public string ObservableName { get; }

        public BindingDefinition(object targetInstance, Member assignmentMember, string observableName)
        {
            TargetInstance = targetInstance;
            AssignmentMember = assignmentMember;
            ObservableName = observableName;
        }
    }
}