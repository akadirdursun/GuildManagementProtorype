using UnityEngine;

namespace AdventurerVillage.CraftingSystem
{
    [CreateAssetMenu(fileName = "SelectedCraftingData", menuName = "Adventurer Village/Crafting System/Selected Crafting Data")]
    public class SelectedCraftingData : ScriptableObject
    {
        public CraftingInfo SelectedCraftingInfo { get; private set; }

        public void SelectCraftingInfo(CraftingInfo craftingInfo)
        {
            SelectedCraftingInfo = craftingInfo;
        }

        public void Clear()
        {
            SelectedCraftingInfo = null;
        }
    }
}