namespace OmniGui.Xaml
{
    using System;
    using System.Reflection;
    using OmniXaml;

    public class OmniGuiXamlBuilder : ExtendedObjectBuilder
    {
        public OmniGuiXamlBuilder(IInstanceCreator creator, ObjectBuilderContext objectBuilderContext, IContextFactory contextFactory) : base(creator, objectBuilderContext, contextFactory)
        {
        }

        protected override void PerformAssigment(Assignment assignment, BuildContext buildContext)
        {
            var model = new Model();

            var bd = assignment.Value as BindingDefinition;
            if (bd != null)
            {
                CreateBinding(model, bd);
            }
            else
            {
                base.PerformAssigment(assignment, buildContext);
            }            
        }

        private void CreateBinding(object model, BindingDefinition assignment)
        {
            var sourceProp = model.GetType().GetRuntimeProperty(assignment.ObservableName);
            var sourceObs = (IObservable<object>) sourceProp.GetValue(model);

            var targetObj = (Layout) assignment.TargetInstance;
            var observer = targetObj.GetSubject(assignment.AssignmentMember.MemberName);

            sourceObs.Subscribe(observer);
        }
    }
}