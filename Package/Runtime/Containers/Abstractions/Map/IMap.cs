using System.Collections.Generic;

namespace Utils.Containers
{
    public interface IMap<TKey, TData> : 
        IROMap<TKey, TData>, 
        IConsumer<KeyValuePair<TKey, TData>>
    {
        bool Add(TKey key, TData element);
        bool Remove(TKey key);
    }

    public interface IConcurrentMap<TKey, TData> : 
        IMap<TKey, TData>,
        IROConcurrentMap<TKey, TData>,
        IConcurrentConsumer<KeyValuePair<TKey, TData>>
    {
    }
}