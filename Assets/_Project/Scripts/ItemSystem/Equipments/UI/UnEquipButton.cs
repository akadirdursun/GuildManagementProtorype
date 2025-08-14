using AdventurerVillage.CharacterSystem;
using AdventurerVillage.InventorySystem;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.ItemSystem.Equipments.UI
{
    public class UnEquipButton : MonoBehaviour
    {
        [SerializeField] private SelectedCharacterData selectedCharacterData;
        [SerializeField] private SelectedEquipmentSlotData selectedEquipmentSlotData;
        [SerializeField] private InventoryStorage inventoryStorage;
        [SerializeField] private Button button;

        private void OnButtonClicked()
        {
            var characterInfo = selectedCharacterData.SelectedCharacterInfo;
            characterInfo.UnequippedItem(selectedEquipmentSlotData.SelectedEquipmentSlotType, out var unequippedEquipment);
            if (unequippedEquipment == null) return;
            inventoryStorage.PlayerInventory.AddItem(unequippedEquipment);
        }

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnButtonClicked);
        }

        #endregion
    }
}