namespace Containers
{
    /// <summary>
    /// Элементы изымаются в порядке добавления
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public interface IQueue<TData> : IOrderedCollection<TData>
    {
        bool Peek(out TData? value);
    }

    public interface ISingleReaderWriterConcurrentQueue<TData> : IQueue<TData>, ISingleReaderWriterConcurrentUnorderedCollection<TData>
    {
    }

    public interface IConcurrentQueue<TData> : ISingleReaderWriterConcurrentQueue<TData>, IConcurrentUnorderedCollection<TData>
    {
    }
}
