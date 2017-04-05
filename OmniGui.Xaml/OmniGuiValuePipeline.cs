using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using OmniXaml;
using OmniXaml.Rework;
using System.Reactive.Linq;

namespace OmniGui.Xaml
{
    using System.Reflection;
    using Zafiro.PropertySystem;

    public class OmniGuiValuePipeline : ValuePipeline
    {
        private readonly IPropertyEngine propertyEngine;
        private readonly IDictionary<BindDefinition, IDisposable> bindings = new Dictionary<BindDefinition, IDisposable>();


        public OmniGuiValuePipeline(IValuePipeline inner, IPropertyEngine propertyEngine) : base(inner)
        {
            this.propertyEngine = propertyEngine;
        }

        protected override void HandleCore(object parent, Member member, MutablePipelineUnit mutable)
        {
            if (member is AttachedProperty)
            {
                RegisterAttachedProperty(member);
            }

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

        private void RegisterAttachedProperty(Member member)
        {
            var type = member.Owner;
            var registerMethod = type.GetRuntimeMethod("RegisterAttached", new[] {typeof(IPropertyEngine)});
            registerMethod?.Invoke(null, new object[] {propertyEngine});
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