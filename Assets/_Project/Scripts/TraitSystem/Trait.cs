using AdventurerVillage.EffectSystem;
using UnityEngine;
using CharacterInfo = AdventurerVillage.CharacterSystem.CharacterInfo;

namespace AdventurerVillage.TraitSystem
{
    [CreateAssetMenu(fileName = "NewTrait", menuName = "Adventurer Village/Trait System/Trait")]
    public class Trait : ScriptableObject
    {
        [SerializeField] protected string traitName;
        [SerializeField] protected Sprite icon;
        [SerializeField] protected EffectList effectList;

        public Sprite Icon => icon;

        public void ApplyTraitEffects(CharacterInfo characterInfo)
        {
            foreach (var effect in effectList.Effects)
            {
                effect.ApplyEffect(characterInfo, name);
            }
        }
    }
}