using System;
using AdventurerVillage.SaveSystem;
using UnityEngine;

namespace AdventurerVillage.TimeSystem
{
    [CreateAssetMenu(fileName = "DateData", menuName = "Adventurer Village/Time System/Date Data")]
    public class DateData : SavableScriptableObject
    {
        [SerializeField] private TimeSettings timeSettings;
        private int _currentTickCount;
        private int _currentHour;
        private int _currentDay = 1;
        private int _currentMonth = 1;
        private int _currentYear = 1;

        public static Action OnDateChanged;
        public static Action OnTickPasses;
        public static Action OnHourPassed;
        public static Action OnDayPassed;
        public float PassedPercentOfDay => _currentHour / (float)timeSettings.DayPerHour;
        public int CurrentYear => _currentYear;

        public string Date => $"D {_currentDay}/ M {_currentMonth}/ Y {_currentYear}";

        public void TimeTick()
        {
            _currentTickCount++;
            OnTickPasses?.Invoke();
            if (_currentTickCount != timeSettings.TickPerHour) return;
            _currentTickCount = 0;
            _currentHour++;
            if (_currentHour == timeSettings.DayPerHour)
            {
                _currentHour = 0;
                _currentDay++;
                if (_currentDay > timeSettings.MonthPerDay)
                {
                    _currentDay = 1;
                    _currentMonth++;
                    if (_currentMonth > timeSettings.YearPerMonth)
                    {
                        _currentMonth = 1;
                        _currentYear++;
                    }
                }

                OnDayPassed?.Invoke();
                OnDateChanged?.Invoke();
            }

            OnHourPassed?.Invoke();
        }

        public string TickToTimeFormat(int tickCount, bool withSuffix = true)
        {
            var ts = "";
            var hasYear = tickCount >= timeSettings.TickPerYear;
            var hasMonth = tickCount >= timeSettings.TickPerMonth;
            var hasDay = tickCount >= timeSettings.TickPerDay;
            if (hasYear)
            {
                var yearCount = tickCount / timeSettings.TickPerYear;
                tickCount %= timeSettings.TickPerYear;
                ts += $"{yearCount}Y ";
            }

            if (hasYear || hasMonth)
            {
                var monthCount = tickCount / timeSettings.TickPerMonth;
                tickCount %= timeSettings.TickPerMonth;
                ts += $"{monthCount}M ";
                if (hasYear)
                    return ts;
            }

            if (hasMonth || hasDay)
            {
                var dayCount = tickCount / timeSettings.TickPerDay;
                tickCount %= timeSettings.TickPerDay;
                ts += $"{dayCount}D ";
                if (hasMonth)
                    return ts;
            }

            var totalHour = tickCount / timeSettings.TickPerHour;
            ts += $"{totalHour}h ";
            return ts;
        }

        public override void Reset()
        {
            _currentTickCount = 0;
            _currentHour = 0;
            _currentDay = 1;
            _currentMonth = 1;
            _currentYear = 1;
        }

        #region Save System

        public override void Save()
        {
            //ES3.Save("_currentTickCount", _currentTickCount);
            //ES3.Save("_currentHour", _currentHour);
            //ES3.Save("_currentDay", _currentDay);
            //ES3.Save("_currentMonth", _currentMonth);
            //ES3.Save("_currentYear", _currentYear);
        }

        public override void Load()
        {
            //_currentTickCount = ES3.Load("_currentTickCount", 0);
            //_currentHour = ES3.Load("_currentHour", 0);
            //_currentDay = ES3.Load("_currentDay", 1);
            //_currentMonth = ES3.Load("_currentMonth", 1);
            //_currentYear = ES3.Load("_currentYear", 1);
        }

        #endregion
    }
}