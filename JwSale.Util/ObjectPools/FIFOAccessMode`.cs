using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace JwSale.Util.ObjectPools
{
    public sealed class FIFOAccessMode<T> : Queue<T>, IAccessMode<T>
    {
        private readonly int _capacity;
        private readonly Func<T> _func;
        private int _count;

        public FIFOAccessMode(int capacity, Func<T> func) : base(capacity)
        {
            _capacity = capacity;
            _func = func;
            InitialQueue();
        }

        public T Rent()
        {
            Interlocked.Increment(ref _count);
            return _capacity < _count ? _func.Invoke() : Dequeue();
        }

        public void Return(T item)
        {
            if (_count > _capacity)
            {
                var disposable = (IDisposable)item;
                disposable.Dispose();
            }
            else
            {
                Enqueue(item);
            }
            Interlocked.Decrement(ref _count);
        }

        private void InitialQueue()
        {
            for (var i = 0; i < _capacity; i++)
            {
                Enqueue(_func.Invoke());
            }
        }
    }
}
