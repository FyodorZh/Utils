namespace Containers
{
    /// <summary>
    /// Источник данных
    /// </summary>
    public interface IProducer<TData>
    {
        /// <summary>
        /// Получает очередной элемент из продюсера
        /// </summary>
        /// <param name="data"> возвращаемый элемент </param>
        /// <returns> FALSE если очередной элемент получить не удалось </returns>
        bool Take(out TData? data);
    }

    /// <summary>
    /// Многопоточный источник данных
    /// </summary>
    public interface IConcurrentProducer<TData> : IProducer<TData>
    {
    }
}