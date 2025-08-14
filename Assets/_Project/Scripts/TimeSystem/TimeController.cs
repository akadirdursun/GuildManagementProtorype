using System;
using System.Collections;
using UnityEngine;

namespace AdventurerVillage.TimeSystem
{
    public class TimeController : MonoBehaviour
    {
        [SerializeField] private TimeSettings timeSettings;
        [SerializeField] private DateData dateData;

        public static Action<float> TimeTick;

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitUntil(() => !timeSettings.IsPaused);
                yield return new WaitForSecondsRealtime(timeSettings.TickTime / timeSettings.TimeSpeedMultiplier);
                dateData.TimeTick();
                TimeTick?.Invoke(timeSettings.TickTime);
            }
        }

        [ContextMenu("NormalSpeed")]
        private void NormalSpeed()
        {
            timeSettings.SetTimeSpeedMultiplier(1);
        }
        [ContextMenu("Speed2X")]
        private void Speed2X()
        {
            timeSettings.SetTimeSpeedMultiplier(2);
        }
        [ContextMenu("Speed3X")]
        private void Speed3X()
        {
            timeSettings.SetTimeSpeedMultiplier(3);
        }
    }
}