using System;
using System.Linq;
using AdventurerVillage.CharacterSystem;
using UnityEngine;

namespace AdventurerVillage.ItemSystem.Equipments.UI
{
    public class EquipmentSelectionArea : MonoBehaviour
    {
        [SerializeField] private SelectedCharacterData selectedCharacterData;
        [SerializeField] private EquipmentTypeInventory equipmentTypeInventory;
        [SerializeField] private CanvasGroup selectClassWarningCanvasGroup;
        [SerializeField] private EquipmentTypeSlotView[] equipmentTypeSlotViews;

        public void Initialize()
        {
            CheckForCharacterClass();

            var characterEquipmentInfo = selectedCharacterData.SelectedCharacterInfo.Equipments;
            var equipmentSlots = characterEquipmentInfo.EquipmentSlots;
            foreach (var slotView in equipmentTypeSlotViews)
            {
                var slot = equipmentSlots.FirstOrDefault(slot => slot.EquipmentSlotType == slotView.EquipmentSlotType);
                slotView.Initialize(slot);
            }
        }

        private void CheckForCharacterClass()
        {
            if (selectedCharacterData.SelectedCharacterInfo == null) return;

            if (selectedCharacterData.SelectedCharacterInfo.ClassAssigned)
            {
                selectClassWarningCanvasGroup.alpha = 0f;
                selectClassWarningCanvasGroup.interactable = false;
                selectClassWarningCanvasGroup.blocksRaycasts = false;
            }
            else
            {
                selectClassWarningCanvasGroup.alpha = 1f;
                selectClassWarningCanvasGroup.interactable = true;
                selectClassWarningCanvasGroup.blocksRaycasts = true;
            }
        }

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            CheckForCharacterClass();
        }

        private void Start()
        {
            foreach (var slotView in equipmentTypeSlotViews)
            {
                slotView.Initialize(equipmentTypeInventory.ShowInventory);
            }
        }

        #endregion
    }
}