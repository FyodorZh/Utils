namespace Containers
{
    public interface IIterable<out TData>
    {
        IIteratorContext<TData>? TryGetIterator(out long iterator);
    }
    
    public interface IStableIterable<out TData> : IIterable<TData>
    {
        IIteratorContext<TData>? TryGetReverseIterator(out long iterator);
    }
}