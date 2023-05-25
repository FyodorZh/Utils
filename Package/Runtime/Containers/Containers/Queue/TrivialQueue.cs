using System;
using System.Collections.Generic;

namespace Containers
{
    public class TrivialQueue<TData> : IQueue<TData>
    {
        private readonly Queue<TData> _queue = new Queue<TData>();

        public bool Add(TData data)
        {
            _queue.Enqueue(data);
            return true;
        }
        
        public bool Peek(out TData? value)
        {
            if (_queue.Count > 0)
            {
                value = _queue.Peek();
                return true;
            }

            value = default;
            return false;
        }

        public bool Take(out TData? data)
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

        public IIteratorContext<TData> GetIterator(out long iterator)
        {
            throw new NotImplementedException();
        }
        
        public IIteratorContext<TData>? GetReverseIterator(out long iterator)
        {
            iterator = 0;
            return null;
        }
    }
}