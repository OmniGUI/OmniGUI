namespace OmniGui.Xaml
{
    using OmniXaml;

    public class OmniGuiXamlBuilder : ExtendedObjectBuilder
    {
        public OmniGuiXamlBuilder(IInstanceCreator creator, ObjectBuilderContext objectBuilderContext, IContextFactory contextFactory) : base(creator, objectBuilderContext, contextFactory)
        {
        }

        protected override void PerformAssigment(Assignment assignment, BuildContext buildContext)
        {
            if (assignment.Value is BindingDefinition)
            {

            }
            else
            {
                base.PerformAssigment(assignment, buildContext);
            }            
        }
    }
}