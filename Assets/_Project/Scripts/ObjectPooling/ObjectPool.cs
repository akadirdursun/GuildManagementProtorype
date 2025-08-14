using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace ADK.Common.ObjectPooling
{
    public class ObjectPool<T>: IObjectPool<T> where T : Object
    {
        private readonly T objectPrefab;
        private readonly Queue<T> passiveObjects = new ();
        private readonly List<T> activeObjects = new ();
        private readonly Action<T,ObjectPool<T>> createAct;
        private readonly Action<T> getAct;
        private readonly Action<T> releaseAct;
        public int PassiveObjectCount => passiveObjects.Count;
        public int ActiveObjectCount => activeObjects.Count;
        public IReadOnlyList<T> ActiveObjectList => activeObjects;

        public ObjectPool(T prefab, int startPoolSize, Action<T, ObjectPool<T>> createAct = null, Action<T> getAct = null,
            Action<T> releaseAct = null)
        {
            objectPrefab = prefab;
            this.createAct = createAct;
            this.getAct = getAct;
            this.releaseAct = releaseAct;
            for (int i = 0; i < startPoolSize; i++)
            {
                passiveObjects.Enqueue(CreateItem());
            }
        }
        
        public T Get()
        {
            T item = PassiveObjectCount != 0 ? passiveObjects.Dequeue() : CreateItem();
            activeObjects.Add(item);
            getAct?.Invoke(item);
            return item;
        }

        public void Release(T item)
        {
            if (!activeObjects.Contains(item)) return;
            activeObjects.Remove(item);
            passiveObjects.Enqueue(item);
            releaseAct?.Invoke(item);
        }

        public void ReleaseAll()
        {
            if (ActiveObjectCount == 0) return;

            for (int i = ActiveObjectCount - 1; i >= 0; i--)
            {
                Release(activeObjects[i]);
            }
        }

        private T CreateItem()
        {
            T newItem = Object.Instantiate(objectPrefab);
            createAct?.Invoke(newItem, this);
            return newItem;
        }
    }
}