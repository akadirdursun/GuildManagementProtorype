using System;
using AdventurerVillage.SaveSystem;
using AdventurerVillage.TimeSystem;
using UnityEngine;

namespace AdventurerVillage.GateSystem
{
    [CreateAssetMenu(fileName = "GateData", menuName = "Adventurer Village/Gate System/Gate Point Data")]
    public class GatePointData : SavableScriptableObject
    {
        [SerializeField] private GateSpawnerConfig gateSpawnerConfig;

        public static Action SpawnNewGate;

        private float _currentGatePoint;

        private void Initialize()
        {
            DateData.OnDayPassed += OnDayPassed;
        }

        private void OnDayPassed()
        {
            var dailyPoint = gateSpawnerConfig.GetDailyPoint();
            _currentGatePoint += dailyPoint;
            if (_currentGatePoint < gateSpawnerConfig.GatePerPoint) return;
            SpawnNewGate?.Invoke();
            _currentGatePoint -= gateSpawnerConfig.GatePerPoint;
        }

        #region Save Methods

        public override void Save()
        {
            //ES3.Save("CurrentGatePoint", _currentGatePoint);
        }

        public override void Load()
        {
            //_currentGatePoint = ES3.Load("CurrentGatePoint", 0f);
            Initialize();
        }

        public override void Reset()
        {
            _currentGatePoint = 0f;
        }

        #endregion
    }
}