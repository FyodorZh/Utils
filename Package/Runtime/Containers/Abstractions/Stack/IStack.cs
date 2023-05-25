namespace Containers
{
    /// <summary>
    /// Элементы изымаются в порядке LIFO
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public interface IStack<TData> : IUnorderedCollection<TData>
    {
        bool Peek(out TData data);
    }

    public interface IConcurrentStack<TData> : IStack<TData>, IConcurrentUnorderedCollection<TData>
    {
    }
}