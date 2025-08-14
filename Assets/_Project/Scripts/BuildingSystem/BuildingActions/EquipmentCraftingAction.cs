using AdventurerVillage.CraftingSystem;

namespace AdventurerVillage.BuildingSystem
{
    public class EquipmentCraftingAction : BaseBuildingAction
    {
        #region Constructor

        public EquipmentCraftingAction(CraftingInfo craftingInfo)
        {
            CraftingInfo = craftingInfo;
        }

        #endregion

        public CraftingInfo CraftingInfo { get; private set; }
    }
}