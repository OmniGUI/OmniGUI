namespace OmniGui.Xaml
{
    using System;
    using System.ComponentModel;
    using System.Reactive.Linq;
    using System.Reflection;
    using Zafiro.PropertySystem.Standard;

    internal class DataContextSubscription : IDisposable
    {
        private IDisposable valueChangeSubs;
        private readonly IDisposable dataContextSubs;

        public DataContextSubscription(BindDefinition bd)
        {
            var targetObj = (Layout)bd.TargetInstance;
            var obs = targetObj.GetChangedObservable(Layout.DataContextProperty);

            dataContextSubs = obs.Where(o => o != null)
                .Subscribe(model =>
                {
                    valueChangeSubs?.Dispose();
                    if (bd.TargetFollowsSource)
                    {
                        valueChangeSubs = SubscribeTargetToSource(bd.SourceProperty, model, targetObj,
                            targetObj.GetProperty(bd.TargetMember.MemberName));
                    }

                    if (bd.SourceFollowsTarget)
                    {
                        valueChangeSubs = SubscribeSourceToTarget(bd.SourceProperty, model, targetObj,
                            targetObj.GetProperty(bd.TargetMember.MemberName));
                    }                    
                });
        }

        private static IDisposable SubscribeSourceToTarget(string modelProperty, object model, Layout layout,
            ExtendedProperty property)
        {
            var obs = layout.GetChangedObservable(property);
            return obs.Subscribe(o =>
            {
                var propInfo = model.GetType().GetRuntimeProperty(modelProperty);
                propInfo.SetValue(model, o);
            });
        }

        private static IDisposable SubscribeTargetToSource(string sourceMemberName, object sourceObject, Layout target,
            ExtendedProperty property)
        {
            var currentValue = sourceObject.GetType().GetRuntimeProperty(sourceMemberName).GetValue(sourceObject);

            var notifyProp = (INotifyPropertyChanged)sourceObject;

            return Observable
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

        public void Dispose()
        {
            valueChangeSubs?.Dispose();
            dataContextSubs?.Dispose();
        }
    }
}