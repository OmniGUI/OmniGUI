using System.Collections;
using System.Collections.Generic;

namespace OmniGui
{
    public class OwnedList<T>  : IEnumerable<T> where T : IChild
    {
        private readonly object parent;
        private readonly IList<T> inner = new List<T>();

        public OwnedList(object parent)
        {
            this.parent = parent;
        }

        public void Add(T child)
        {
            child.Parent = parent;
            inner.Add(child);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return inner.GetEnumerator();
        }
    }
}