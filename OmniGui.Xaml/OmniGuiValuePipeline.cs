using OmniGui.OmniGui;

namespace OmniGui.Xaml
{
    using System.Reflection;
    using Zafiro.PropertySystem;
    using System;
    using System.Collections.Generic;
    using System.Reactive.Disposables;
    using OmniXaml;
    using System.Reactive.Linq;


    public class OmniGuiValuePipeline : ValuePipeline
    {
        private readonly IDictionary<BindDefinition, IDisposable> bindings = new Dictionary<BindDefinition, IDisposable>();


        public OmniGuiValuePipeline(IValuePipeline inner) : base(inner)
        {
        }

        protected override void HandleCore(object parent, Member member, MutablePipelineUnit mutable, INodeToObjectBuilder builder, BuilderContext context)
        {
            var od = mutable.Value as ObserveDefinition;
            var bd = mutable.Value as BindDefinition;

            if (od != null)
            {
                //BindToObservable(od);
            }
            else if (bd != null)
            {
                if (bd.TargetInstance is IPropertyHost)
                {
                    ClearExistingBinding(bd);
                    var bindingSubscription = BindToProperty(context, bd);
                    AddExistingBinding(bd, bindingSubscription);
                }

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

        private IDisposable BindToProperty(BuilderContext buildContext, BindDefinition bd)
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

        private static IDisposable BindToTemplatedParent(BuilderContext buildContext, BindDefinition bd)
        {
            var source = (Layout)buildContext.Store["TemplatedParent"];
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