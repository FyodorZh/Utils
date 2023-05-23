namespace Containers
{
    /// <summary>
    /// Коробка в которую можно клсать и изымать элементы, порядок изъятия не определён
    /// </summary>
    /// <typeparam name="TData"> Тип элементов </typeparam>
    public interface IUnorderedCollection<TData> : 
        IROUnorderedCollection<TData>, 
        IConsumer<TData>, 
        IProducer<TData>
    {
        void Clear();
    }

    /// <summary>
    /// Допускает конкурентное добавление и изъятие элементов двумя потоками.
    /// Один поток кладёт, другой - изымает
    /// </summary>
    public interface ISingleReaderWriterConcurrentUnorderedCollection<TData> : IUnorderedCollection<TData>
    {
    }

    /// <summary>
    /// Допускает полностью асинхронную работу.
    /// </summary>
    public interface IConcurrentUnorderedCollection<TData> : ISingleReaderWriterConcurrentUnorderedCollection<TData>, IConcurrentConsumer<TData>, IConcurrentProducer<TData>
    {
    }
}