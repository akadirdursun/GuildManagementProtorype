using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AKD.Common.GameEventSystem
{
    [CreateAssetMenu(menuName = "AKD/Common/Game Event System/Game Event", fileName = "NewGameEvent")]
    public class GameEvent : ScriptableObject
    {
        [SerializeField] private bool logEnabled;
        private List<UnityEvent> events = new();

        public void Invoke()
        {
            for (int i = 0; i < events.Count; i++)
            {
                var e = events[i];
                if (logEnabled)
                    Debug.Log($"{name} Event Called!");
                e.Invoke();
            }
        }


        public void AddEvent(UnityEvent e)
        {
            if (events.Contains(e)) return;

            events.Add(e);
        }

        public void RemoveEvent(UnityEvent e)
        {
            if (events.Contains(e))
                events.Remove(e);
        }
    }
}