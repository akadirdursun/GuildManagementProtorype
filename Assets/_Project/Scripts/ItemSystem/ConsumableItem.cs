using AdventurerVillage.LevelSystem;
using UnityEngine;
using CharacterInfo = AdventurerVillage.CharacterSystem.CharacterInfo;

namespace AdventurerVillage.ItemSystem
{
    public abstract class ConsumableItem : Item
    {
        public abstract void Consume(CharacterInfo characterInfo, int consumableItemGrade);
        public abstract string GetItemEffectInfo(Grade itemGrade);
    }
}