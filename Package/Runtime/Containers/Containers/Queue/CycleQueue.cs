using System;
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

        public bool Put(TData value)
        {
            if (Grow())
            {
                _data[(_index + _count) & _capacityMask] = value;
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
        
        public bool TryPeek(out TData? value)
        {
            if (_count > 0)
            {
                value = _data[_index];
                return true;
            }
            value = default;
            return false;
        }

        public bool TryPop(out TData? value)
        {
            if (_count > 0)
            {
                value = _data[_index];
                _data[_index] = default!;
                _count -= 1;
                _index = (_index + 1) & _capacityMask;
                return true;
            }
            value = default;
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

        public TData this[int id]
        {
            get
            {
                if ((uint) id >= (uint)_count) 
                {
                    throw new ArgumentOutOfRangeException();
                }
                return _data[(_index + id) & _capacityMask];
            }
            set
            {
                if ((uint) id >= (uint)_count) 
                {
                    throw new ArgumentOutOfRangeException();
                }
                
                _data[(_index + id) & _capacityMask] = value;
            }
        }
        
        public IIteratorContext<TData>? TryGetIterator(out long iterator)
        {
            iterator = 0;
            return null;
        }
        
        public IIteratorContext<TData>? TryGetReverseIterator(out long iterator)
        {
            iterator = 0;
            return null;
        }

        // public EnumerableWithOrder Enumerate()
        // {
        //     return new EnumerableWithOrder(this, QueueEnumerationOrder.HeadToTail);
        // }
        //
        // public EnumerableWithOrder Enumerate(QueueEnumerationOrder order)
        // {
        //     return new EnumerableWithOrder(this, order);
        // }
        //
        // public struct EnumerableWithOrder
        // {
        //     private readonly CycleQueue<T> mQueue;
        //     private readonly QueueEnumerationOrder mOrder;
        //
        //     internal EnumerableWithOrder(CycleQueue<T> queue, QueueEnumerationOrder order)
        //     {
        //         mQueue = queue;
        //         mOrder = order;
        //     }
        //
        //     public Enumerator GetEnumerator()
        //     {
        //         return new Enumerator(mQueue, mOrder);
        //     }
        // }
        //
        // public struct Enumerator
        // {
        //     private readonly CycleQueue<T> mQueue;
        //     private readonly QueueEnumerationOrder mOrder;
        //     private int mCurrent;
        //     private int mEnd;
        //
        //     internal Enumerator(CycleQueue<T> queue, QueueEnumerationOrder order)
        //     {
        //         mQueue = queue;
        //         mOrder = order;
        //         mCurrent = 0;
        //         mEnd = 0;
        //         SetIndices();
        //     }
        //
        //     private void SetIndices()
        //     {
        //         switch (mOrder)
        //         {
        //             case QueueEnumerationOrder.HeadToTail:
        //                 mCurrent = mQueue._id - 1;
        //                 mEnd = mQueue._id + mQueue._count - 1;
        //                 break;
        //             case QueueEnumerationOrder.TailToHead:
        //                 mCurrent = mQueue._id + mQueue._count;
        //                 mEnd = mQueue._id;
        //                 break;
        //             default:
        //                 mCurrent = 0;
        //                 mEnd = 0;
        //                 break;
        //         }
        //     }
        //
        //     public void Dispose()
        //     {
        //     }
        //
        //     public bool MoveNext()
        //     {
        //         switch (mOrder)
        //         {
        //             case QueueEnumerationOrder.HeadToTail:
        //                 if (mCurrent == mEnd)
        //                 {
        //                     return false;
        //                 }
        //                 else
        //                 {
        //                     mCurrent++;
        //                     return true;
        //                 }
        //             case QueueEnumerationOrder.TailToHead:
        //                 if (mCurrent == mEnd)
        //                 {
        //                     return false;
        //                 }
        //                 else
        //                 {
        //                     mCurrent--;
        //                     return true;
        //                 }
        //             default:
        //                 return false;
        //         }
        //     }
        //
        //     public T Current
        //     {
        //         get
        //         {
        //             return mQueue._data[mCurrent & mQueue._capacityMask];
        //         }
        //     }
        // }
        //
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
