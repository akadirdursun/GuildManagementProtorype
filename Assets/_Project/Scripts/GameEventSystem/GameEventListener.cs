using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace AKD.Common.GameEventSystem
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private GameEvent[] gameEvents;
        [SerializeField, Space] private UnityEvent response;

        private void OnEnable()
        {
            foreach (GameEvent gameEvent in gameEvents)
            {
                gameEvent.AddEvent(response);
            }
        }

        private void OnDisable()
        {
            foreach (GameEvent gameEvent in gameEvents)
            {
                gameEvent.RemoveEvent(response);
            }
        }
    }
}