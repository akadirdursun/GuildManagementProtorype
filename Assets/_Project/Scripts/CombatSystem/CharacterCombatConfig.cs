using System;
using AdventurerVillage.CharacterSystem;

namespace AdventurerVillage.CombatSystem
{
    [Serializable]
    public class CharacterCombatConfig
    {
        public CharacterCombatConfig(CharacterInfo characterInfo)
        {
            CharacterInfo = characterInfo;
            var characterStats = CharacterInfo.Stats;
            BeforeCombatInfo = new CharacterBeforeCombatInfo()
            {
                StrengthStat = characterStats.Strength.Level,
                ConstitutionStat = characterStats.Constitution.Level,
                AgilityStat = characterStats.Agility.Level,
                IntelligenceStat = characterStats.Intelligence.Level,
                MagicStat = characterStats.Magic.Level
            };
        }

        public readonly CharacterInfo CharacterInfo;
        public float CurrentAggro;
        public float AttackCooldown;
        public CharacterBeforeCombatInfo BeforeCombatInfo { get; private set; }
    }
}