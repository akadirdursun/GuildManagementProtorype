using UnityEngine;
using UnityEngine.Events;

namespace AKD.Common.GameEventSystem
{
    public class GenericGameEventListener<T, GE> : MonoBehaviour, IGenericGameEventListener<T> where GE: GenericGameEvent<T>
    {
        [SerializeField] private GE[] gameEvents;
        [SerializeField] private UnityEvent<T> response;

        private void OnEnable()
        {
            for (int i = gameEvents.Length - 1; i >= 0; i--)
            {
                gameEvents[i].RegisterListener(this);
            }
        }

        private void OnDisable()
        {
            for (int i = gameEvents.Length - 1; i >= 0; i--)
            {
                gameEvents[i].UnregisterListener(this);
            }
        }

        public void Invoke(T value)
        {
            response?.Invoke(value);
        }
    }
}