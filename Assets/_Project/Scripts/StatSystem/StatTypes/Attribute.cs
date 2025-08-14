using System;
using AdventurerVillage.LevelSystem;
using AdventurerVillage.Utilities;
using UnityEngine;

namespace AdventurerVillage.StatSystem
{
    [Serializable]
    public class Attribute
    {
        #region Constructors

        public Attribute()
        {
            
        }
        public Attribute(int currentLevel, Func<float, Grade> calculateGradeFunc)
        {
            this.currentLevel = currentLevel;
            _calculateGradeFunc = calculateGradeFunc;
            grade = _calculateGradeFunc.Invoke(Level);
        }

        #endregion


        [SerializeField, ReadOnly] private int currentLevel;
        [SerializeField, ReadOnly] private Grade grade;

        private Func<float, Grade> _calculateGradeFunc;

        public const int MaxLevel = 100;
        public Action OnCharacterStatChanged;

        public Grade Grade => grade;
        public int Level => currentLevel;

        private void Increase(int increaseBy)
        {
            currentLevel = Mathf.Clamp(currentLevel + increaseBy, 0, MaxLevel);
            grade = _calculateGradeFunc.Invoke(Level);
            OnCharacterStatChanged?.Invoke();
        }
    }
}