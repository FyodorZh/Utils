namespace Containers
{
    public interface IROSet<TData> :
        IIterable<TData>,
        ICountable
    {
        bool Contains(TData element);
    }

    public interface IROConcurrentSet<TData> : IROSet<TData>
    {
    }
}
