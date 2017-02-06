using System;
using System.Collections;
using System.Collections.Generic;
using DynamicData;

namespace OmniGui
{
    public class OwnedList<T> : IEnumerable<T> where T : IChild
    {
        private readonly SourceList<T> inner = new SourceList<T>();
        private readonly object parent;
        private IDisposable subs;

        public OwnedList(object parent)
        {
            this.parent = parent;
        }

        public int Count => inner.Count;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return inner.Items.GetEnumerator();
        }

        public void OnChildAdded(Action<T> action)
        {
            subs = inner.Connect().OnItemAdded(action)
                .Subscribe();
        }

        public void Add(T child)
        {
            child.Parent = parent;
            inner.Add(child);
        }

        public void Clear()
        {
            inner.Clear();
        }
    }
}