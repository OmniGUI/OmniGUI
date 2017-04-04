using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using OmniXaml;
using OmniXaml.Rework;
using System.Reactive.Linq;

namespace OmniGui.Xaml
{
    public class OmniGuiValuePipeline : ValuePipeline
    {
        private readonly IDictionary<BindDefinition, IDisposable> bindings = new Dictionary<BindDefinition, IDisposable>();


        public OmniGuiValuePipeline(IValuePipeline inner) : base(inner)
        {
        }

        protected override void HandleCore(object parent, Member member, MutablePipelineUnit mutable)
        {
            var od = mutable.Value as ObserveDefinition;
            var bd = mutable.Value as BindDefinition;

            if (od != null)
            {
                //BindToObservable(od);
            }
            else if (bd != null)
            {
                ClearExistingBinding(bd);
                var bindingSubscription = BindToProperty(null, bd);
                AddExistingBinding(bd, bindingSubscription);
                mutable.Handled = true;
            }
        }

        private void ClearExistingBinding(BindDefinition bd)
        {
            if (bindings.ContainsKey(bd))
            {
                bindings[bd].Dispose();
                bindings.Remove(bd);
            }
        }

        private void AddExistingBinding(BindDefinition bd, IDisposable subs)
        {
            bindings.Add(bd, subs);
        }

        private IDisposable BindToProperty(BuildContext buildContext, BindDefinition bd)
        {
            if (bd.Source == BindingSource.DataContext)
            {
                return BindToDataContext(bd);
            }
            else
            {
                return BindToTemplatedParent(buildContext, bd);
            }
        }

        private IDisposable BindToDataContext(BindDefinition bd)
        {
            return new DataContextSubscription(bd);
        }

        private static IDisposable BindToTemplatedParent(BuildContext buildContext, BindDefinition bd)
        {
            var source = (Layout)buildContext.Bag["TemplatedParent"];
            if (bd.TargetFollowsSource)
            {
                var property = source.GetProperty(bd.SourceProperty);
                var sourceObs = source.GetChangedObservable(property).StartWith(source.GetValue(property));

                var targetLayout = (Layout)bd.TargetInstance;
                var observer = targetLayout.GetObserver(targetLayout.GetProperty(bd.TargetMember.MemberName));

                sourceObs.Subscribe(observer);
            }

            return Disposable.Empty;
        }
    }
}