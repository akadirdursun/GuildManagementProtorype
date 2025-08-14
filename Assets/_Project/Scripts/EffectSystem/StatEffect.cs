using System;
using AdventurerVillage.EffectSystem.Enums;
using AdventurerVillage.StatSystem;
using CharacterInfo = AdventurerVillage.CharacterSystem.CharacterInfo;

namespace AdventurerVillage.EffectSystem
{
    [Serializable]
    public class StatEffect : IEffect
    {
        #region Constructor

        public StatEffect()
        {
        }

        public StatEffect(StatTypes statType, float effectValue)
        {
            _statType = statType;
            _effectValue = effectValue;
        }

        #endregion

        private StatTypes _statType;
        private float _effectValue;

        public StatTypes StatType => _statType;
        public float EffectValue => _effectValue;

        public void IncreaseEffectValue(float value)
        {
            _effectValue += value;
        }

        public void ApplyEffect(CharacterInfo characterInfo, string effectSource)
        {
            var effectInfo = GetEffectInfo(effectSource);
            var combatStat = GetCombatStat(characterInfo.Stats);
            combatStat.AddEffect(effectInfo);
        }

        public void RemoveEffect(CharacterInfo characterInfo, string effectSource)
        {
            var combatStat = GetCombatStat(characterInfo.Stats);
            combatStat.RemoveEffect(effectSource);
        }

        public EffectInfo GetEffectInfo(string effectSource)
        {
            return new EffectInfo()
            {
                effectType = EffectType.Addition,
                effectSource = effectSource,
                effectValue = _effectValue
            };
        }

        public string GetEffectInfo()
        {
            return $"Increase <link={_statType}>{_statType}</link> by +{_effectValue}";
        }

        private Stat GetCombatStat(CharacterStats characterStats)
        {
            return _statType switch
            {
                StatTypes.Health => characterStats.Health,
                StatTypes.HealthRegen => characterStats.HealthRegeneration,
                StatTypes.Stamina => characterStats.Stamina,
                StatTypes.StaminaRegen => characterStats.StaminaRegeneration,
                StatTypes.Mana => characterStats.Mana,
                StatTypes.ManaRegen => characterStats.ManaRegeneration,
                StatTypes.Damage => characterStats.Damage,
                StatTypes.DamageReductionFlat => characterStats.DamageReductionFlat,
                StatTypes.DamageReductionPercentage => characterStats.DamageReductionPercent,
                StatTypes.DodgeChance => characterStats.DodgeChance,
                StatTypes.CriticalHitChance => characterStats.CriticalHitChance,
                StatTypes.DamageReflect => characterStats.DamageReflect,
                StatTypes.CounterChance => characterStats.CounterChance,
                StatTypes.AttackSpeed => characterStats.AttackSpeed,
                StatTypes.ManaCostReduction => characterStats.ManaCostReduction,
                StatTypes.CraftQuality => characterStats.CraftQuality,
                StatTypes.CraftProductivity => characterStats.CraftProductivity,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}