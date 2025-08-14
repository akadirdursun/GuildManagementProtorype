using System;
using AdventurerVillage.LevelSystem;
using AdventurerVillage.StatSystem;
using UnityEngine;
using CharacterInfo = AdventurerVillage.CharacterSystem.CharacterInfo;

namespace AdventurerVillage.ItemSystem
{
    [CreateAssetMenu(fileName = "NewHealItem", menuName = "Adventurer Village/Item System/Heal Item")]
    public class HealItem : ConsumableItem
    {
        [SerializeField] private HealData[] healEffects;

        public override void Consume(CharacterInfo characterInfo, int consumableItemGrade)
        {
            foreach (var healEffect in healEffects)
            {
                var vital = GetVital(characterInfo.Stats, healEffect.vitalType);
                vital.Recover(healEffect.GetHealValue(consumableItemGrade));
            }
        }

        private VitalStat GetVital(CharacterStats characterStats, VitalTypes vitalType)
        {
            return vitalType switch
            {
                VitalTypes.HealthPoint => characterStats.Health,
                VitalTypes.Stamina => characterStats.Stamina,
                VitalTypes.Mana => characterStats.Mana,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public override string GetItemEffectInfo(Grade itemGrade)
        {
            var effectInfo = "";
            foreach (var effect in healEffects)
            {
                effectInfo += $"• {effect.GetEffectInfo((int)itemGrade)}";
            }

            return effectInfo;
        }
    }
}