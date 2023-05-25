using System;
using System.Collections.Generic;
using System.Diagnostics;
using Utils;

namespace Containers
{
    public class CycleQueue<TData> : IQueue<TData>, IArray<TData>
    {
        private readonly bool _canGrow;

        private int _capacity; // Always power of 2
        private int _capacityMask;

        private int _count;
        private int _index;

        private TData[] _data;

        public CycleQueue(bool canGrow = true)
            : this(16, canGrow)
        {
        }

        public CycleQueue(int capacity, bool canGrow = true)
        {
            _capacity = BitMath.NextPow2((uint)capacity);
            _capacityMask = _capacity - 1;

            _count = 0;
            _index = 0;
            _data = new TData[_capacity];
            _canGrow = canGrow;
        }

        public void Clear()
        {
            _count = 0;
        }

        public int Capacity => _capacity;

        public int Count => _count;

        private bool Grow()
        {
            if (_count == _capacity)
            {
                if (_canGrow)
                {
                    TData[] newData = new TData[_capacity * 2];

                    ArrayCopier<TData>.Copy(_data, _index, newData, 0, _capacity - _index);
                    if (_index != 0)
                    {
                        ArrayCopier<TData>.Copy(_data, 0, newData, _capacity - _index, _index);
                    }

                    _data = newData;

                    _capacity *= 2;
                    _capacityMask = _capacity - 1;

                    _index = 0;
                    return true;
                }

                return false;
            }

            return true;
        }

        public bool Add(TData data)
        {
            if (Grow())
            {
                _data[(_index + _count) & _capacityMask] = data;
                _count += 1;
                return true;
            }

            return false;
        }

        public bool EnqueueToHead(TData value)
        {
            if (Grow())
            {
                _index = (_index + _capacity - 1) & _capacityMask;
                _data[_index] = value;
                _count += 1;
                return true;
            }

            return false;
        }
        
        public bool Peek(out TData? value)
        {
            if (_count > 0)
            {
                value = _data[_index];
                return true;
            }
            value = default;
            return false;
        }

        public bool Take(out TData? data)
        {
            if (_count > 0)
            {
                data = _data[_index];
                _data[_index] = default!;
                _count -= 1;
                _index = (_index + 1) & _capacityMask;
                return true;
            }
            data = default;
            return false;
        }

        public TData Head
        {
            get
            {
                Debug.Assert(_count > 0);
                return _data[_index];
            }
        }

        public TData Tail
        {
            get
            {
                Debug.Assert(_count > 0);
                return _data[(_index + _count - 1) & _capacityMask];
            }
        }

        public TData this[int index]
        {
            get
            {
                if ((uint) index >= (uint)_count) 
                {
                    throw new ArgumentOutOfRangeException();
                }
                return _data[(_index + index) & _capacityMask];
            }
            set
            {
                if ((uint) index >= (uint)_count) 
                {
                    throw new ArgumentOutOfRangeException();
                }
                
                _data[(_index + index) & _capacityMask] = value;
            }
        }
        
        public IIteratorContext<TData> GetIterator(out long iterator)
        {
            throw new NotImplementedException();
        }
        
        public IIteratorContext<TData>? GetReverseIterator(out long iterator)
        {
            iterator = 0;
            return null;
        }

        // [UT.UT("CycleQueue")]
        // private static void UT(UT.IUTest test)
        // {
        //     CycleQueue<int> queue = new CycleQueue<int>(2);
        //     test.Equal(queue.Count, 0);
        //
        //     int output;
        //
        //     queue.Put(1);
        //     test.Equal(queue.Count, 1);
        //     test.Equal(queue.Head, queue.Tail);
        //     test.Equal(queue.Tail, queue[0]);
        //
        //     test.Equal(queue.TryPop(out output), true);
        //     test.Equal(output, 1);
        //
        //     queue.Put(1);
        //     queue.Put(2);
        //     test.Equal(queue.Count, 2);
        //     test.Equal(queue.Head, 1);
        //     test.Equal(queue.Tail, 2);
        //     test.Equal(queue[0], 1);
        //     test.Equal(queue[1], 2);
        //
        //     queue.Put(3);
        //
        //     Log.d("HEAD TO TAIL");
        //     foreach (var q in queue.Enumerate(QueueEnumerationOrder.HeadToTail))
        //     {
        //         Log.d("{0}", q);
        //     }
        //
        //     Log.d("TAIL TO HEAD");
        //     foreach (var q in queue.Enumerate(QueueEnumerationOrder.TailToHead))
        //     {
        //         Log.d("{0}", q);
        //     }
        //
        //     test.Equal(queue[2], 3);
        //
        //     test.Equal(queue.TryPop(out output), true);
        //     test.Equal(output, 1);
        //
        //     test.Equal(queue[0], 2);
        //
        //     test.Equal(queue.TryPop(out output), true);
        //     test.Equal(output, 2);
        //
        //     test.Equal(queue.TryPop(out output), true);
        //     test.Equal(output, 3);
        // }
    }
}
