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
        /// <param name="value"> возвращаемый элемент </param>
        /// <returns> FALSE если очередной элемент получить не удалось </returns>
        bool TryPop(out TData? value);
    }

    /// <summary>
    /// Многопоточный источник данных
    /// </summary>
    public interface IConcurrentProducer<TData> : IProducer<TData>
    {
    }
}