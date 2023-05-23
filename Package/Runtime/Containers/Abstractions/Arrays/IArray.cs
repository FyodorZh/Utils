namespace Containers
{
    public interface IArray<TData> : IROArray<TData>
    {
        new TData this[int id] { get; set; }
    }
}