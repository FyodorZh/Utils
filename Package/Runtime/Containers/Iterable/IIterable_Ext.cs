using System;

namespace Containers
{
    public static class IIterable_Ext
    {
        public static Enumerable<TData> EnumerateUnstable<TData>(this IIterable<TData> iterable)
        {
            var context = iterable.GetIterator(out long iterator);
            return new Enumerable<TData>(new Enumerator<TData>(context, iterator));
        }
        
        public static Enumerable<TData> Enumerate<TData>(this IStableIterable<TData> iterable)
        {
            var context = iterable.GetIterator(out long iterator);
            return new Enumerable<TData>(new Enumerator<TData>(context, iterator));
        }
        
        public static Enumerable<TData> EnumerateReverse<TData>(this IStableIterable<TData> iterable)
        {
            var context = iterable.GetReverseIterator(out long iterator);
            if (context != null)
            {
                return new Enumerable<TData>(new Enumerator<TData>(context, iterator));
            }
            throw new NotSupportedException();
        }
        
        public readonly struct Enumerable<TData>
        {
            private readonly Enumerator<TData> _enumerator;

            public Enumerable(Enumerator<TData> enumerator)
            {
                _enumerator = enumerator;
            }

            public Enumerator<TData> GetEnumerator()
            {
                return _enumerator;
            }
        }
    }
}