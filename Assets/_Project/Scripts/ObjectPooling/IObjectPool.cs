using System.Collections.Generic;

namespace ADK.Common.ObjectPooling
{
    public interface IObjectPool<T>
    {
        public int ActiveObjectCount { get; }
        public int PassiveObjectCount { get; }
        public IReadOnlyList<T> ActiveObjectList { get; }
        public T Get();
        public void Release(T item);
        public void ReleaseAll();
    }
}