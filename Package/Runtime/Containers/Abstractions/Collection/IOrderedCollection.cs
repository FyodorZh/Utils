namespace Utils.Containers
{
    public interface IOrderedCollection<TData> : 
        IUnorderedCollection<TData>, 
        IROOrderedCollection<TData>
    {
        
    }
}