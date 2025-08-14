using System;
using AdventurerVillage.LevelSystem;
using AdventurerVillage.StatSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AdventurerVillage.ItemSystem.Equipments
{
    [Serializable]
    public struct EquipmentGradeConfig
    {
        [SerializeField] private Grade grade;
        [SerializeField] private float maxDurability;
        [SerializeField/*, ListDrawerSettings(ListElementLabelName = "statType")*/] private EquipmentCombatStats[] guaranteedStatTypes;
        [SerializeField/*, ListDrawerSettings(ListElementLabelName = "statType")*/] private EquipmentCombatStats[] possibleStatTypes;
        [SerializeField, Range(0f, 1f)] private float possibleStatProbability;

        public Grade Grade => grade;
        public float MaxDurability => maxDurability;
        public EquipmentCombatStats[] GuaranteedStatTypes => guaranteedStatTypes;

        public EquipmentCombatStats[] GetRandomCombatStatArray()
        {
            var array = Random.Range(0f, 1f) <= possibleStatProbability ? possibleStatTypes : guaranteedStatTypes;
            return array;
        }
    }

    [Serializable]
    public struct EquipmentCombatStats
    {
        public StatTypes statType;
    }
}