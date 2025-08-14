using System;
using AdventurerVillage.LevelSystem;

namespace AdventurerVillage.TraitSystem
{
    [Serializable]
    public class AcquiredTraitInfo
    {
        public AcquiredTraitInfo(Trait trait, LevelWithExperience levelWithExperience)
        {
            this.trait = trait;
            this.levelWithExperience = levelWithExperience;
        }

        private Trait trait;
        private LevelWithExperience levelWithExperience;

        public Action OnLevelUp;
        public Trait Trait => trait;
        public int Level => levelWithExperience.currentLevel;

        public void OnExperienceGained(float value)
        {
        }
    }
}