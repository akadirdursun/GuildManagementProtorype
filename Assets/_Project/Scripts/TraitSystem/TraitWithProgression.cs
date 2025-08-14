using AdventurerVillage.LevelSystem;
using UnityEngine;

namespace AdventurerVillage.TraitSystem
{
    [CreateAssetMenu(fileName = "NewTraitWithProgression", menuName = "Adventurer Village/Trait System/Trait With Progression")]
    public class TraitWithProgression : Trait
    {
        [SerializeField] private LevelWithExperience levelWithExperienceProgress;
        public LevelWithExperience TraitLevelWithExperienceProgress => levelWithExperienceProgress;
    }
}