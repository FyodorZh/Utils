namespace Containers
{
    /// <summary>
    /// Элементы изымаются в порядке LIFO
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public interface IStack<TData> : IUnorderedCollection<TData>
    {
        bool TryPeek(out TData data);
    }

    public interface IConcurrentStack<TData> : IStack<TData>, IConcurrentUnorderedCollection<TData>
    {
    }
}