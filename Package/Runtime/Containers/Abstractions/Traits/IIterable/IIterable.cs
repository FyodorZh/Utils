namespace Containers
{
    public interface IIterable<out TData>
    {
        IIteratorContext<TData> GetIterator(out long iterator);
    }
    
    public interface IStableIterable<out TData> : IIterable<TData>
    {
        IIteratorContext<TData>? GetReverseIterator(out long iterator);
    }
}