using System;

namespace Containers
{
    public interface IIteratorContext<out TData> : IDisposable
    {
        TData? GetNext(ref long iterator, out bool found);
    }
}