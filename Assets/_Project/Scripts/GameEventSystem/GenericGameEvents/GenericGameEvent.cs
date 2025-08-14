using System.Collections.Generic;
using UnityEngine;

namespace AKD.Common.GameEventSystem
{
    public class GenericGameEvent<T> : ScriptableObject
    {
        [SerializeField] private bool logsEnabled;
        private List<IGenericGameEventListener<T>> _listeners = new();

        public void Invoke(T t)
        {
            if (logsEnabled)
                Debug.Log($"<color = yellow>{name}</color> Invoked. Value: {t}");

            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                if (_listeners[i] == null)
                {
                    Debug.LogError($"{name} object type of {GetType()} event listener index {i} is null.");
                    continue;
                }
                _listeners[i].Invoke(t);
            }
        }

        public void RegisterListener(IGenericGameEventListener<T> listener)
        {
            _listeners.Add(listener);
        }

        public void UnregisterListener(IGenericGameEventListener<T> listener)
        {
            if (!_listeners.Contains(listener)) return;
            _listeners.Remove(listener);
        }
    }
}