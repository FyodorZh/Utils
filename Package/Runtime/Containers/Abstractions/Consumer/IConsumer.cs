namespace Containers
{
    /// <summary>
    /// Потребитель данных
    /// </summary>
    public interface IConsumer<in TData>
    {
        /// <summary>
        /// Кладёт элемент в приёмник.
        /// </summary>
        /// <param name="data"></param>
        /// <returns> FALSE если положить элемент не удалось. Например, исчерпана ёмкость коллекции или что угодно другое </returns>
        bool Put(TData data);
    }
    
    /// <summary>
    /// Многопоточный потребитель данных
    /// </summary>
    public interface IConcurrentConsumer<in TData> : IConsumer<TData>
    {
    }
}