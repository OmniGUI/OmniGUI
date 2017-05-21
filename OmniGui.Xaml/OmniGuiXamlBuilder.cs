//namespace OmniGui.Xaml
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Reactive.Disposables;
//    using System.Reactive.Linq;
//    using System.Reflection;
//    using OmniXaml;

//    public class OmniGuiXamlBuilder : ExtendedObjectBuilder
//    {
//        private readonly IDictionary<BindDefinition, IDisposable> bindings = new Dictionary<BindDefinition, IDisposable>();

//        public OmniGuiXamlBuilder(IInstanceCreator creator, ObjectBuilderContext objectBuilderContext,
//            IContextFactory contextFactory) : base(creator, objectBuilderContext, contextFactory)
//        {
//        }

//        protected override void PerformAssigment(Assignment assignment, BuildContext buildContext)
//        {
//            var od = assignment.Value as ObserveDefinition;
//            var bd = assignment.Value as BindDefinition;

//            if (od != null)
//            {
//                BindToObservable(od);
//            }
//            else if (bd != null)
//            {
//                ClearExistingBinding(bd);
//                var bindingSubscription = BindToProperty(buildContext, bd);
//                AddExistingBinding(bd, bindingSubscription);
//            }
//            else
//            {
//                base.PerformAssigment(assignment, buildContext);
//            }
//        }

//        private void AddExistingBinding(BindDefinition bd, IDisposable subs)
//        {
//            bindings.Add(bd, subs);
//        }

//        private void ClearExistingBinding(BindDefinition bd)
//        {
//            if (bindings.ContainsKey(bd))
//            {
//                bindings[bd].Dispose();
//                bindings.Remove(bd);
//            }
//        }

//        private IDisposable BindToProperty(BuildContext buildContext, BindDefinition bd)
//        {
//            if (bd.Source == BindingSource.DataContext)
//            {
//                return BindToDataContext(bd);
//            }
//            else
//            {
//                return BindToTemplatedParent(buildContext, bd);
//            }
//        }

//        private static IDisposable BindToTemplatedParent(BuildContext buildContext, BindDefinition bd)
//        {
//            var source = (Layout) buildContext.Bag["TemplatedParent"];
//            if (bd.TargetFollowsSource)
//            {
//                var property = source.GetProperty(bd.SourceProperty);
//                var sourceObs = source.GetChangedObservable(property).StartWith(source.GetValue(property));

//                var targetLayout = (Layout) bd.TargetInstance;
//                var observer = targetLayout.GetObserver(targetLayout.GetProperty(bd.TargetMember.MemberName));

//                sourceObs.Subscribe(observer);
//            }

//            return Disposable.Empty;
//        }

//        private IDisposable BindToDataContext(BindDefinition bd)
//        {
//            return new DataContextSubscription(bd);
//        }    

//        private static void BindToObservable(ObserveDefinition definition)
//        {
//            var targetObj = (Layout) definition.TargetInstance;
//            var obs = targetObj.GetChangedObservable(Layout.DataContextProperty);
//            obs.Where(o => o != null)
//                .Subscribe(context =>
//                {
//                    BindSourceObservableToTarget(definition, context, targetObj);
//                    //BindTargetToSource(definition, context, targetObj);
//                });
//        }

//        private static void BindSourceObservableToTarget(ObserveDefinition assignment, object context, Layout targetObj)
//        {
//            var sourceProp = context.GetType().GetRuntimeProperty(assignment.ObservableName);
//            var sourceObs = (IObservable<object>) sourceProp.GetValue(context);
//            var targetProperty = targetObj.GetProperty(assignment.AssignmentMember.MemberName);
//            var observer = targetObj.GetObserver(targetProperty);

//            sourceObs.Subscribe(observer);
//        }

//        private static void BindTargetToSource(ObserveDefinition assignment, object context, Layout targetObj)
//        {
//            var sourceProp = context.GetType().GetRuntimeProperty(assignment.ObservableName);
//            var sourceObs = (IObserver<object>) sourceProp.GetValue(context);
//            var targetProperty = targetObj.GetProperty(assignment.AssignmentMember.MemberName);
//            var observer = targetObj.GetChangedObservable(targetProperty);

//            observer.Subscribe(sourceObs);
//        }
//    }
//}