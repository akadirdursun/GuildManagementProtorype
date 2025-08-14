using AdventurerVillage.LevelSystem;
using AdventurerVillage.StatSystem;
using UnityEngine;
using CharacterInfo = AdventurerVillage.CharacterSystem.CharacterInfo;

namespace AdventurerVillage.Utilities
{
    public static class LevelUtility
    {
        public static readonly float WeightBaseValue = 1.5f;
        public static float CalculateCharacterLevel(this CharacterStats characterStats)
        {
            var characterAttributes = new []
            {
                characterStats.Strength,
                characterStats.Constitution,
                characterStats.Agility,
                characterStats.Intelligence,
                characterStats.Magic
            };
            return CalculateCharacterLevel(characterAttributes);
        }

        public static float CalculateCharacterLevel(this Attribute[] characterStats)
        {
            var sumOfWeights = 0f;
            var weightedStatSum = 0f;

            foreach (var stat in characterStats)
            {
                var weight = Mathf.Pow(WeightBaseValue, (int)stat.Grade);
                weightedStatSum += stat.Level * weight;
                sumOfWeights += weight;
            }

            return weightedStatSum / sumOfWeights;
        }

        public static float CalculateCharacterLevel(this CharacterStatLevelInfo[] levelInfos)
        {
            var sumOfWeights = 0f;
            var weightedStatSum = 0f;

            foreach (var levelInfo in levelInfos)
            {
                var weight = Mathf.Pow(WeightBaseValue, (int)levelInfo.Grade);
                weightedStatSum += levelInfo.Level * weight;
                sumOfWeights += weight;
            }

            return weightedStatSum / sumOfWeights;
        }
    }
}