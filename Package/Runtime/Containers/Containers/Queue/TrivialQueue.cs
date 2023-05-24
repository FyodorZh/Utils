using System.Collections.Generic;

namespace Containers
{
    public class TrivialQueue<TData> : IQueue<TData>
    {
        private readonly Queue<TData> _queue = new Queue<TData>();

        public bool Put(TData data)
        {
            _queue.Enqueue(data);
            return true;
        }
        
        public bool TryPeek(out TData? value)
        {
            if (_queue.Count > 0)
            {
                value = _queue.Peek();
                return true;
            }

            value = default;
            return false;
        }

        public bool TryPop(out TData? data)
        {
            if (_queue.Count > 0)
            {
                data = _queue.Dequeue();
                return true;
            }

            data = default;
            return false;
        }
        
        public void Clear()
        {
            _queue.Clear();
        }

        public IIteratorContext<TData>? TryGetIterator(out long iterator)
        {
            iterator = 0;
            return null;
        }
        
        public IIteratorContext<TData>? TryGetReverseIterator(out long iterator)
        {
            iterator = 0;
            return null;
        }
    }
}