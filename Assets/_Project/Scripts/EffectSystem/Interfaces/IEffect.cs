using AdventurerVillage.CharacterSystem;

namespace AdventurerVillage.EffectSystem
{
    public interface IEffect
    {
        public void ApplyEffect(CharacterInfo characterInfo, string effectSource);
    }
}