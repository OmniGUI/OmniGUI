namespace OmniGui.VisualStates
{
    using System;
    using System.Linq;
    using System.Reactive;
    using System.Reactive.Linq;
    using DynamicData;
    using Zafiro.PropertySystem;

    public class TriggerCollection
    {
        private readonly SourceList<StateTrigger> sourceList;

        public TriggerCollection(IPropertyEngine propertyEngine)
        {
            sourceList = new SourceList<StateTrigger>();
            var observable = sourceList.Connect();
            var activeChanged = observable.MergeMany(trigger => propertyEngine.GetChangedObservable(StateTrigger.IsActiveProperty, trigger))
                .ToUnit()
                .StartWith(Unit.Default);

            var collectionChanged = observable.ToCollection();


            IsActive = collectionChanged.CombineLatest(activeChanged, (items, _) =>
            {
                return items.Any(state => state.IsActive);
            }).DistinctUntilChanged();
        }

        public IObservable<bool> IsActive { get; }

        public void Add(StateTrigger stateTrigger)
        {
            sourceList.Add(stateTrigger);
        }
    }
}