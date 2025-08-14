using UnityEngine;

namespace AdventurerVillage.TimeSystem
{
    [CreateAssetMenu(menuName = "Adventurer Village/Time System/Time Settings", fileName = "TimeSettings")]
    public class TimeSettings : ScriptableObject
    {
        [SerializeField] private float tickTime = 0.1f;
        [SerializeField] private int tickPerHour = 10;
        [SerializeField] private int dayPerHour;
        [SerializeField] private int monthPerDay;
        [SerializeField] private int yearPerMonth;

        public float TickTime => tickTime;
        public int TickPerHour => tickPerHour;
        public int TickPerDay => dayPerHour * tickPerHour;
        public int TickPerMonth => monthPerDay * TickPerDay;
        public int TickPerYear => yearPerMonth * TickPerMonth;
        public int DayPerHour => dayPerHour;
        public int MonthPerDay => monthPerDay;
        public int YearPerMonth => yearPerMonth;
        public int TimeSpeedMultiplier { get; private set; } = 1;
        public bool IsPaused { get; private set; } = false;

        public void SetTimeSpeedMultiplier(int timeSpeedMultiplier)
        {
            if (timeSpeedMultiplier < 0)
            {
                Debug.LogError("Time speed multiplier cannot be negative");
                timeSpeedMultiplier = 0;
            }

            IsPaused = timeSpeedMultiplier == 0;
            if (IsPaused) return;
            TimeSpeedMultiplier = timeSpeedMultiplier;
        }
    }
}