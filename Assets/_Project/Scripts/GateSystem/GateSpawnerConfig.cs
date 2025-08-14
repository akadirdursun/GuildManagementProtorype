using AdventurerVillage.CharacterSystem;
using AdventurerVillage.TimeSystem;
using UnityEngine;

namespace AdventurerVillage.GateSystem
{
    [CreateAssetMenu(fileName = "GateConfig", menuName = "Adventurer Village/Gate System/Gate Spawner Config")]
    public class GateSpawnerConfig : ScriptableObject
    {
        [SerializeField] private DateData dateData;
        [SerializeField] private CharacterDatabase characterDatabase;
        [SerializeField, Space /*, TabGroup("Gate Settings"), ListDrawerSettings(ListElementLabelName = "grade")*/]
        private GateData[] gateData;
        [SerializeField /*, TabGroup("Point Settings")*/] private int maxGateCount = 15;
        [SerializeField /*, TabGroup("Point Settings")*/] private int gatePerPoint = 1000;
        [SerializeField /*, TabGroup("Point Settings")*/] private float maxDailyPoint = 250;
        [SerializeField /*, TabGroup("Point Settings")*/] private float baseMaxDailyPoint = 11;
        [SerializeField /*, TabGroup("Point Settings")*/] private float baseMinDailyPoint = 10;
        [SerializeField /*, TabGroup("Point Settings")*/] private float extraPointPerYear = 10;
        [SerializeField, Range(1.01f, 5f) /*, TabGroup("Point Settings")*/] private float basePointPerCharacter = 1.1f;
        [SerializeField /*, TabGroup("Position Settings")*/] private int minDistanceToCity = 3;
        [SerializeField /*, TabGroup("Position Settings")*/] private int maxDistanceToCity = 5;
        [SerializeField /*, TabGroup("Position Settings")*/] private int maxDistanceIncreasePerYear = 2;

        public GateData[] GateData => gateData;
        public int MaxGateCount => maxGateCount;
        public int GatePerPoint => gatePerPoint;
        public int MinDistanceToCity => minDistanceToCity;
        public int MaxDistanceToCity => maxDistanceToCity + dateData.CurrentYear * maxDistanceIncreasePerYear;

        public float GetDailyPoint()
        {
            var randomPoint = Random.Range(baseMinDailyPoint, baseMaxDailyPoint);
            var extraPoint = (dateData.CurrentYear - 1) * extraPointPerYear;
            var characterPoints = 0f;
            var characterGradeGroups = characterDatabase.CountCharacters();
            foreach (var group in characterGradeGroups)
            {
                var groupPower = (int)group.Key;
                var multiplier = Mathf.Pow(basePointPerCharacter, groupPower);
                var totalPoints = group.Value * multiplier;
                characterPoints += totalPoints;
            }

            return Mathf.Clamp(randomPoint + extraPoint + characterPoints, 0, maxDailyPoint);
        }

        #region ScriptableObject Methods

        private void OnValidate()
        {
            if (minDistanceToCity >= maxDistanceToCity)
                maxDistanceToCity = minDistanceToCity + 1;

            if (baseMaxDailyPoint <= maxDailyPoint) return;
            baseMaxDailyPoint = maxDailyPoint;

            if (baseMaxDailyPoint > baseMinDailyPoint) return;
            if (baseMaxDailyPoint <= maxDailyPoint)
            {
                baseMinDailyPoint = baseMaxDailyPoint - 0.1f;
                return;
            }

            baseMaxDailyPoint = baseMinDailyPoint + 0.1f;
        }

        #endregion
    }
}