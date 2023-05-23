using System.Collections.Generic;

namespace Utils.Containers
{
    public interface IROMap<TKey, TData> :
        IIterable<KeyValuePair<TKey, TData>>,
        ICountable
    {
        bool TryGetValue(TKey key, out TData element);
    }

    public interface IROConcurrentMap<TKey, TData> : IROMap<TKey, TData>
    {
    }
}