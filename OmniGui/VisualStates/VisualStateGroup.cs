namespace OmniGui.VisualStates
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Reactive;
    using System.Reactive.Linq;
    using Zafiro.PropertySystem;

    public class VisualStateGroup
    {
        private readonly IPropertyEngine propertyEngine;

        public VisualStateGroup(IPropertyEngine propertyEngine)
        {
            this.propertyEngine = propertyEngine;
            StateTriggers = new TriggerCollection(propertyEngine);
            StateTriggers.IsActive.Subscribe(ToggleSetters);
        }

        private void ToggleSetters(bool shouldActivate)
        {
            foreach (var setter in Setters)
            {
                setter.Apply();
            }
        }

        public TriggerCollection StateTriggers { get; }
        public SetterCollection Setters { get; set; }
    }

    public static class ObservableEx
    {
        public static IObservable<Unit> ToUnit<T>(this IObservable<T> source)
        {
            return source.Select(_ => Unit.Default);
        }
    }
}