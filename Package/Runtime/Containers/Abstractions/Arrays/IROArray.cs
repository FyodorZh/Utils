namespace Containers
{
    public interface IROArray<out TData> : IStableIterable<TData>, ICountable
    {
        TData this[int index] { get; }
    }
}