namespace Containers
{
    public static class IProducer_Ext
    {
        public static void ForAll<TData>(this IProducer<TData> self, System.Action<TData> processor)
        {
            while (self.TryPop(out var value))
            {
                processor(value!);
            }
        }

        public static AsEnumerable<TObject> Enumerate<TObject>(this IProducer<TObject> producer)
        {
            return new AsEnumerable<TObject>(producer);
        }

        public struct AsEnumerable<TObject>
        {
            private readonly IProducer<TObject> _producer;

            public AsEnumerable(IProducer<TObject> producer)
            {
                _producer = producer;
            }

            public Enumerator<TObject> GetEnumerator()
            {
                return new Enumerator<TObject>(_producer);
            }
        }

        public struct Enumerator<TObject>// : IEnumerator<TObject>
        {
            private readonly IProducer<TObject> _producer;
            private TObject? _current;

            public Enumerator(IProducer<TObject> producer)
            {
                _producer = producer;
                _current = default;
            }

            public bool MoveNext()
            {
                return _producer.TryPop(out _current);
            }

            public TObject? Current => _current;
        }
    }
}