using System.Reactive.Linq;

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


            var bd = assignment.Value as BindingDefinition;
            if (bd != null)
            {
                CreateBinding(bd);
            }
            else
            {
                base.PerformAssigment(assignment, buildContext);
            }            
        }

        private void CreateBinding(BindingDefinition assignment)
        {
            var targetObj = (Layout) assignment.TargetInstance;
            var obs = targetObj.GetChangedObservable(Layout.DataContextProperty);
            obs.Where(o => o != null)
                .Subscribe(context =>
            {
                var sourceProp = context.GetType().GetRuntimeProperty(assignment.ObservableName);
                var sourceObs = (IObservable<object>)sourceProp.GetValue(context);
                var extProp = targetObj.GetProperty(assignment.AssignmentMember.MemberName);
                var observer = targetObj.GetObserver(extProp);

                sourceObs.Subscribe(observer);
            });
        }
    }
}