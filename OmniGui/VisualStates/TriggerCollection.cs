namespace OmniGui.VisualStates
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive;
    using System.Reactive.Linq;
    using DynamicData;
    using Zafiro.PropertySystem;

    public class TriggerCollection : ICollection<StateTrigger>
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

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(StateTrigger item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(StateTrigger[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(StateTrigger item)
        {
            throw new NotImplementedException();
        }

        public int Count => sourceList.Count;

        public bool IsReadOnly => false;

        public IEnumerator<StateTrigger> GetEnumerator()
        {
            return sourceList.Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}