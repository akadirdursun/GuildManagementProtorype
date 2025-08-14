using System;
using UnityEngine;

namespace AdventurerVillage.LevelSystem
{
    [Serializable]
    public class LevelWithExperience
    {
        public int currentLevel;
        public float currentExperience;
        public AnimationCurve experienceCurve;
        public Action OnLevelUp;
        public Action OnExperienceChanged;
        public int MaxLevel=>(int)experienceCurve.keys[experienceCurve.length - 1].time;

        public float TargetExperience => experienceCurve.Evaluate(currentLevel);

        public void Initialize(int level)
        {
            currentLevel = level;
        }   

        public void AddExperience(float experience)
        {
            if (currentLevel == MaxLevel) return;
            currentExperience += experience;
            TryToLevelUp();
            OnExperienceChanged?.Invoke();
        }

        private void TryToLevelUp()
        {
            if (currentExperience < TargetExperience) return;
            currentExperience -= TargetExperience;
            currentLevel++;
            OnLevelUp?.Invoke();
        }

        public LevelWithExperience Copy()
        {
            return MemberwiseClone() as LevelWithExperience;
        }
    }
}