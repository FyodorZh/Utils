namespace Containers
{
    public interface IROOrderedCollection<out TData> : 
        IROUnorderedCollection<TData>,
        IStableIterable<TData>
    {
        
    }
}