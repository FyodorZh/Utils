using System;
using System.Collections;
using System.Collections.Generic;

namespace Containers
{
    public struct Enumerator<TData> : IEnumerator<TData>
    {
        private readonly IIteratorContext<TData> _iteratorContext;
        private long _iterator;
        private bool _disposed;
        
        public TData Current { get; private set; }

        object IEnumerator.Current => Current;
        
        public Enumerator(IIteratorContext<TData> iteratorContext, long iterator)
        {
            _iteratorContext = iteratorContext;
            _iterator = iterator;
            Current = default!;
            _disposed = false;
        }
        
        public bool MoveNext()
        {
            Current = _iteratorContext.GetNext(ref _iterator, out var found);
            return found;
        }

        public void Reset()
        {
            throw new InvalidOperationException();
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _iteratorContext.Dispose();
                _disposed = true;
            }
        }
    }
}