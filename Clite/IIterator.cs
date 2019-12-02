using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clite
{
    interface IIterator<T>
    {
        bool HasNext();
        T Next();
    }

    class IteratorAdapter<T> : IIterator<T>
    {
        private readonly IEnumerator<T> e;
        private bool? hasNext;

        public IteratorAdapter(IEnumerable<T> e)
        {
            this.e = e.GetEnumerator();
        }

        public bool HasNext()
        {
            if (hasNext == null)
                hasNext = e.MoveNext();
            return hasNext.Value;
        }

        public T Next()
        {
            if (!HasNext())
                throw new InvalidOperationException();
            hasNext = null;
            return e.Current;
        }
    }

    class ListIterator<T> : IIterator<T>
    {
        private readonly List<T> list;
        private int index;

        public ListIterator(List<T> list)
        {
            this.list = list;
        }

        public bool HasNext()
        {
            return index < list.Count;
        }

        public T Next()
        {
            if (!HasNext())
                throw new InvalidOperationException();
            return list[index++];
        }
    }
}
