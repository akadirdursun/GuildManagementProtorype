using System;
using AdventurerVillage.EnemySystem.Enum;
using AdventurerVillage.LevelSystem;
using AdventurerVillage.StatSystem;
using UnityEngine;

namespace AdventurerVillage.EnemySystem
{
    [Serializable]
    public class EnemyInfo
    {
        #region Constructors

        public EnemyInfo()
        {
        }

        public EnemyInfo(string name, EnemyCombatTypes combatType, Grade grade,CharacterStats stats)
        {
            this.name = name;
            this.combatType = combatType;
            this.grade = grade;
            this.stats = stats;
        }

        #endregion

        [SerializeField] private string name;
        [SerializeField] private EnemyCombatTypes combatType;
        [SerializeField] private Grade grade;
        [SerializeField] private CharacterStats stats;

        public string Name => name;
        public EnemyCombatTypes CombatType => combatType;
        public Grade Grade => grade;
        public CharacterStats Stats => stats;
    }
}