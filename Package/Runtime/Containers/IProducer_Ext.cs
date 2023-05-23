namespace Containers
{
    public static class IProducer_Ext
    {
        public static void ForAll<TData>(this IProducer<TData> self, System.Action<TData> processor)
        {
            while (self.TryPop(out var value))
            {
                processor(value);
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
            private readonly IProducer<TObject> mProducer;
            private TObject mCurrent;

            public Enumerator(IProducer<TObject> producer)
            {
                mProducer = producer;
                mCurrent = default(TObject);
            }

            public bool MoveNext()
            {
                return mProducer.TryPop(out mCurrent);
            }

            public TObject Current
            {
                get { return mCurrent; }
            }
        }
    }
}