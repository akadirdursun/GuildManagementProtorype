using System;
using System.Linq;
using AdventurerVillage.StatSystem;
using UnityEngine;

namespace AdventurerVillage.ItemSystem
{
    [Serializable]
    public struct HealData
    {
        public VitalTypes vitalType;
        //[ListDrawerSettings(DraggableItems = false, ShowIndexLabels = true)]
        [SerializeField]
        private float[] healValuesByLevel;

        public float GetHealValue(int grade = 0)
        {
            if (grade >= healValuesByLevel.Length) return healValuesByLevel.Last();
            return healValuesByLevel[grade];
        }

        public string GetEffectInfo(int itemLevel)
        {
            return $"Heal <link={vitalType}>{vitalType}</link> for {healValuesByLevel[itemLevel]}";
        }
    }
}