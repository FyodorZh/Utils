namespace Containers
{
    public static class IROArray_Ext
    {
        public static AsEnumerable<TObject> Enumerate<TObject>(this IROArray<TObject> list)
        {
            return new AsEnumerable<TObject>(list);
        }

        public readonly struct AsEnumerable<TObject>
        {
            private readonly IROArray<TObject> _list;

            public AsEnumerable(IROArray<TObject> list)
            {
                _list = list;
            }

            public Enumerator<TObject> GetEnumerator()
            {
                return new Enumerator<TObject>(_list);
            }
        }

        public struct Enumerator<TObject>// : IEnumerator<TObject>
        {
            private readonly IROArray<TObject> _list;
            private readonly int _count;
            private int _index;

            public Enumerator(IROArray<TObject> list)
            {
                _list = list;
                _count = list.Count;
                _index = -1;
            }

            public bool MoveNext()
            {
                _index += 1;
                return _index < _count;
            }

            public TObject? Current
            {
                get
                {
                    if (_index >= 0 && _index < _count)
                    {
                        return _list[_index];
                    }

                    return default;
                }
            }
        }
    }
}