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
                ClearExistingBinding(bd);
                BindToProperty(buildContext, bd);
                AddExistingBinding(bd);
            }
            else
            {
                base.PerformAssigment(assignment, buildContext);
            }
        }

        private void AddExistingBinding(BindDefinition bd)
        {            
        }

        private void ClearExistingBinding(BindDefinition bd)
        {            
        }

        private void BindToProperty(BuildContext buildContext, BindDefinition bd)
        {
            if (bd.Source == BindingSource.DataContext)
            {
                BindToDataContext(bd);
            }
            else
            {
                BindToTemplatedParent(buildContext, bd);
            }
        }

        private static void BindToTemplatedParent(BuildContext buildContext, BindDefinition bd)
        {
            var source = (Layout) buildContext.Bag["TemplatedParent"];
            if (bd.TargetFollowsSource)
            {
                var property = source.GetProperty(bd.SourceProperty);
                var sourceObs = source.GetChangedObservable(property).StartWith(source.GetValue(property));

                var targetLayout = (Layout) bd.TargetInstance;
                var observer = targetLayout.GetObserver(targetLayout.GetProperty(bd.TargetMember.MemberName));

                sourceObs.Subscribe(observer);
            }
        }

        private void BindToDataContext(BindDefinition bd)
        {
            var targetObj = (Layout) bd.TargetInstance;
            var obs = targetObj.GetChangedObservable(Layout.DataContextProperty);

            obs.Where(o => o != null)
                .Subscribe(model =>
                {
                    if (bd.TargetFollowsSource)
                    {
                        SubscribeTargetToSource(bd.SourceProperty, model, targetObj,
                            targetObj.GetProperty(bd.TargetMember.MemberName));
                    }

                    if (bd.SourceFollowsTarget)
                    {
                        SubscribeSourceToTarget(bd.SourceProperty, model, targetObj,
                            targetObj.GetProperty(bd.TargetMember.MemberName));
                    }
                });
        }

        private void BindToProperty(BindDefinition definition, IObservable<object> obs)
        {
            var targetObj = (Layout) definition.TargetInstance;
            obs.Where(o => o != null)
                .Subscribe(model =>
                {
                    if (definition.TargetFollowsSource)
                    {
                        SubscribeTargetToSource(definition.SourceProperty, model, targetObj,
                            targetObj.GetProperty(definition.TargetMember.MemberName));
                    }

                    if (definition.SourceFollowsTarget)
                    {
                        SubscribeSourceToTarget(definition.SourceProperty, model, targetObj,
                            targetObj.GetProperty(definition.TargetMember.MemberName));
                    }
                });
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
                        SubscribeTargetToSource(definition.SourceProperty, model, targetObj,
                            targetObj.GetProperty(definition.TargetMember.MemberName));
                    }

                    if (definition.SourceFollowsTarget)
                    {
                        SubscribeSourceToTarget(definition.SourceProperty, model, targetObj,
                            targetObj.GetProperty(definition.TargetMember.MemberName));
                    }
                });
        }

        private void SubscribeSourceToTarget(string modelProperty, object model, Layout layout,
            ExtendedProperty property)
        {
            var obs = layout.GetChangedObservable(property);
            obs.Subscribe(o =>
            {
                var propInfo = model.GetType().GetRuntimeProperty(modelProperty);
                propInfo.SetValue(model, o);
            });
        }

        private static void SubscribeTargetToSource(string sourceMemberName, object sourceObject, Layout target,
            ExtendedProperty property)
        {
            var currentValue = sourceObject.GetType().GetRuntimeProperty(sourceMemberName).GetValue(sourceObject);

            var notifyProp = (INotifyPropertyChanged) sourceObject;

            Observable
                .FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                    eh => notifyProp.PropertyChanged += eh, ev => notifyProp.PropertyChanged -= ev)
                .Where(pattern => pattern.EventArgs.PropertyName == sourceMemberName)
                .Select(_ => sourceObject.GetType().GetRuntimeProperty(sourceMemberName).GetValue(sourceObject))
                .StartWith(currentValue)
                .Subscribe(value =>
                {
                    target.SetValue(property, value);
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