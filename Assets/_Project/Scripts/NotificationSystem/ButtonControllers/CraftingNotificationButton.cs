using AdventurerVillage.CraftingSystem;
using AdventurerVillage.CraftingSystem.UI;
using AdventurerVillage.ItemSystem.Equipments;
using AdventurerVillage.UI.ScreenControlSystems;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.NotificationSystem
{
    public class CraftingNotificationButton : BaseNotificationButton
    {
        [SerializeField] private EquipmentDatabase equipmentDatabase;
        [SerializeField] private SelectedCraftingData selectedCraftingData;
        [SerializeField] private Image itemImage;
        private CraftingInfo _craftingInfo;

        protected override void OnButtonClicked()
        {
            selectedCraftingData.SelectCraftingInfo(_craftingInfo);
            UIScreenController.Instance.ShowScreen(typeof(ClaimCraftedItemScreen));
        }

        public void Initialize(CraftingInfo craftingInfo)
        {
            _craftingInfo = craftingInfo;
            var equipmentData = equipmentDatabase.GetEquipmentData(_craftingInfo.EquipmentTypeId);
            itemImage.sprite = equipmentData.Icon;
        }
    }
}