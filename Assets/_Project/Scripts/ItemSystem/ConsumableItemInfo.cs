using AdventurerVillage.LevelSystem;
using UnityEngine;

namespace AdventurerVillage.ItemSystem
{
    public class ConsumableItemInfo : ItemInfo
    {
        #region Constructor

        public ConsumableItemInfo(){}
        public ConsumableItemInfo(
            string id,
            string name,
            Grade grade,
            int itemAmount,
            int maxStackSize) : base(id, name, grade, itemAmount, true, maxStackSize)
        {
        }

        #endregion


        public override Sprite Icon => ItemIconManager.Instance.GetConsumableItemInfo(Id);

        protected override string GetItemEffectInfo()
        {
            return "consumableItem.GetItemEffectInfo(ItemGrade)";
        }

        protected override string GetItemDescription()
        {
            return " consumableItem.Description";
        }
    }
}