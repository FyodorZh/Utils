namespace Containers
{
    public interface IROArray<out TData> : IIterable<TData>, ICountable
    {
        TData this[int id] { get; }
    }
}