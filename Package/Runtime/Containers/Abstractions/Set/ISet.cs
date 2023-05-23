namespace Containers
{
    public interface ISet<TData> : IROSet<TData>, IConsumer<TData>
    {
        bool Remove(TData element);
    }

    public interface IConcurrentSet<TData> : 
        ISet<TData>, 
        IROConcurrentSet<TData>, 
        IConcurrentConsumer<TData>
    {
    }
}
