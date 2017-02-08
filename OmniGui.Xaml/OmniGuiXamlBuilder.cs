namespace OmniGui.Xaml
{
    using System;
    using System.ComponentModel;
    using System.Reactive.Linq;
    using System.Reflection;
    using OmniXaml;
    using Zafiro.PropertySystem.Standard;

    public class OmniGuiXamlBuilder : ExtendedObjectBuilder
    {
        public OmniGuiXamlBuilder(IInstanceCreator creator, ObjectBuilderContext objectBuilderContext,
            IContextFactory contextFactory) : base(creator, objectBuilderContext, contextFactory)
        {
        }

        protected override void PerformAssigment(Assignment assignment, BuildContext buildContext)
        {
            var od = assignment.Value as ObserveDefinition;
            var bd = assignment.Value as BindDefinition;

            if (od != null)
            {
                BindToObservable(od);
            }
            else if (bd != null)
            {
                BindToProperty(bd);
            }
            else
            {
                base.PerformAssigment(assignment, buildContext);
            }
        }

        private void BindToProperty(BindDefinition definition)
        {
            var targetObj = (Layout) definition.TargetInstance;
            var obs = targetObj.GetChangedObservable(Layout.DataContextProperty);
            obs.Where(o => o != null)
                .Subscribe(model =>
                {
                    if (definition.TargetFollowsSource)
                    {
                        SubscribeTargetToSource(definition.SourceProperty, model, targetObj, targetObj.GetProperty(definition.TargetMember.MemberName));
                    }

                    if (definition.SourceFollowsTarget)
                    {
                        SubscribeSourceToTarget(definition.SourceProperty, model, targetObj, targetObj.GetProperty(definition.TargetMember.MemberName));
                    }
                });
        }

        private void SubscribeSourceToTarget(string modelProperty, object model, Layout layout, ExtendedProperty property)
        {
            var obs = layout.GetChangedObservable(property);
            obs.Subscribe(o =>
            {
                var propInfo = model.GetType().GetRuntimeProperty(modelProperty);
                propInfo.SetValue(model, o);
            });
        }

        private static void SubscribeTargetToSource(string sourceMemberName, object sourceObject, Layout target, ExtendedProperty property)
        {
            var notifyProp = (INotifyPropertyChanged) sourceObject;
            var obs = Observable.FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                eh => notifyProp.PropertyChanged += eh, ev => notifyProp.PropertyChanged -= ev);
            obs.Subscribe(pattern =>
            {
                if (pattern.EventArgs.PropertyName == sourceMemberName)
                {
                    var value = sourceObject.GetType().GetRuntimeProperty(sourceMemberName).GetValue(sourceObject);
                    target.SetValue(property, value);
                }
            });
        }

        private static void BindToObservable(ObserveDefinition definition)
        {
            var targetObj = (Layout) definition.TargetInstance;
            var obs = targetObj.GetChangedObservable(Layout.DataContextProperty);
            obs.Where(o => o != null)
                .Subscribe(context =>
                {
                    BindSourceObservableToTarget(definition, context, targetObj);
                    //BindTargetToSource(definition, context, targetObj);
                });
        }

        private static void BindSourceObservableToTarget(ObserveDefinition assignment, object context, Layout targetObj)
        {
            var sourceProp = context.GetType().GetRuntimeProperty(assignment.ObservableName);
            var sourceObs = (IObservable<object>) sourceProp.GetValue(context);
            var targetProperty = targetObj.GetProperty(assignment.AssignmentMember.MemberName);
            var observer = targetObj.GetObserver(targetProperty);

            sourceObs.Subscribe(observer);
        }

        private static void BindTargetToSource(ObserveDefinition assignment, object context, Layout targetObj)
        {
            var sourceProp = context.GetType().GetRuntimeProperty(assignment.ObservableName);
            var sourceObs = (IObserver<object>) sourceProp.GetValue(context);
            var targetProperty = targetObj.GetProperty(assignment.AssignmentMember.MemberName);
            var observer = targetObj.GetChangedObservable(targetProperty);

            observer.Subscribe(sourceObs);
        }
    }
}