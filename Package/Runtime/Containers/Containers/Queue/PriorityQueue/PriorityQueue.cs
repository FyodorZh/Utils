using System;
using System.Collections.Generic;
namespace Containers
{
    /// <summary>
    /// Приоритетная очередь. Чем меньше ключ, тем он приоритетнее
    /// </summary>
    public partial class PriorityQueue<Key, Data> : 
        IUnorderedCollection<KeyValuePair<Key, Data>>,
        IIteratorContext<KeyValuePair<Key, Data>>
        where Key : IComparable<Key>
    {
        private readonly List<Key> _keys = new List<Key>();
        private readonly List<Data> _data = new List<Data>();

        protected virtual void OnClear() { }
        protected virtual void OnAdd(Key key, Data data, int pos) { }
        protected virtual void OnTrimLast() { }
        protected virtual void OnSwap(int posA, int posB) { }

        public PriorityQueue()
        {
            Clear();
        }
        
        public void Clear()
        {
            OnClear();
            _keys.Clear();
            _data.Clear();
            _keys.Add(default!);
            _data.Add(default!);
        }

        public int Count => _keys.Count - 1;

        public void Enqueue(Key key, Data data)
        {
            _keys.Add(key);
            _data.Add(data);
            int pos = _keys.Count - 1;
            OnAdd(key, data, pos);
            Up(pos);
        }

        public Key TopKey()
        {
            if (Count != 0)
            {
                return _keys[1];
            }
            return default!;
        }

        public Data Peek()
        {
            if (Count != 0)
            {
                return _data[1];
            }
            return default!;
        }

        public Data Dequeue()
        {
            TryPop(out var res);
            return res.Value;
        }

        public bool Put(KeyValuePair<Key, Data> data)
        {
            Enqueue(data.Key, data.Value);
            return true;
        }

        public bool TryPeek(out KeyValuePair<Key, Data> value)
        {
            int count = Count;
            if (count != 0)
            {
                value = new KeyValuePair<Key, Data>(_keys[1], _data[1]);
                return true;
            }
            value = default;
            return false;
        }
        
        public bool TryPop(out KeyValuePair<Key, Data> data)
        {
            int count = Count;
            if (count != 0)
            {
                OnSwap(1, count);
                OnTrimLast();
                data = new KeyValuePair<Key, Data>(_keys[1], _data[1]);
                _keys[1] = _keys[count];
                _data[1] = _data[count];
                _keys.RemoveAt(count);
                _data.RemoveAt(count);
                Down(1);
                return true;
            }
            data = default;
            return false;
        }
        
        public IIteratorContext<KeyValuePair<Key, Data>>? TryGetIterator(out long iterator)
        {
            iterator = 0;
            return this;
        }
   
        Key IPriorityQueueCtl.KeyAt(int idx)
        {
            return _keys[idx];
        }

        Data IPriorityQueueCtl.DataAt(int idx)
        {
            return _data[idx];
        }

        bool IPriorityQueueCtl.RemoveAtIndex(int id)
        {
            int lasPos = Count;
            
            if (id < 1 || id > lasPos)
            {
                return false;
            }

            Swap(id, lasPos);
            _keys.RemoveAt(lasPos);
            _data.RemoveAt(lasPos);
            
            OnTrimLast();

            Down(id);
            return true;
        }

        bool IPriorityQueueCtl.UpdateKeyAtIndex(int id, Key key)
        {
            if (id < 1 || id >= _keys.Count)
            {
                return false;
            }
            UpdateKey(id, key);
            return true;
        }
        
        bool IPriorityQueueCtl.UpdateKeyAtIndex(int id, Func<Data, Key> evaluator)
        {
            if (id < 1 || id >= _keys.Count)
            {
                return false;
            }

            UpdateKey(id, evaluator(_data[id]));
            return true;
        }
        
        private void UpdateKey(int id, Key key)
        {
            int cmp = _keys[id].CompareTo(key);
            _keys[id] = key;
            if (cmp < 0)
            {
                Down(id);
            }
            else if (cmp > 0)
            {
                Up(id);
            }
        }

        private void Up(int id)
        {
            while (id > 1)
            {
                int top = id / 2;
                if (_keys[id].CompareTo(_keys[top]) < 0)
                {
                    Swap(id, top);
                    id = top;
                }
                else
                {
                    break;
                }
            }
        }

        private void Down(int id)
        {
            int count = Count;
            while (id <= count)
            {
                int a = id * 2;
                int minAB = id;
                if (a <= count)
                {
                    int b = a + 1;
                    if (b <= count)
                    {
                        minAB = _keys[a].CompareTo(_keys[b]) < 0 ? a : b;
                    }
                    else
                    {
                        minAB = a;
                    }
                }
                if (minAB != id && _keys[minAB].CompareTo(_keys[id]) < 0)
                {
                    Swap(minAB, id);
                    id = minAB;
                }
                else
                {
                    break;
                }
            }
        }

        private void Swap(int a, int b)
        {
            OnSwap(a, b);
            Key k = _keys[a];
            Data d = _data[a];
            _keys[a] = _keys[b];
            _data[a] = _data[b];
            _keys[b] = k;
            _data[b] = d;
        }

        KeyValuePair<Key, Data> IIteratorContext<KeyValuePair<Key, Data>>.GetNext(ref long iterator, out bool found)
        {
            int nextPos = (int)iterator + 1;
            if (nextPos < _keys.Count)
            {
                iterator += 1;
                found = true;
                return new KeyValuePair<Key, Data>(_keys[nextPos], _data[nextPos]);
            }

            found = false;
            return default;
        }

        void IDisposable.Dispose()
        {
            // DO NOTHING
        }
    }
}
