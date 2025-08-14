using System;
using AdventurerVillage.InventorySystem;
using UnityEngine;

namespace AdventurerVillage.ItemSystem.Equipments
{
    [CreateAssetMenu(fileName = "SelectedEquipmentSlotData", menuName = "Adventurer Village/Item System/Equipments/Selected Equipment Slot Data")]
    public class SelectedEquipmentSlotData : ScriptableObject
    {
        private InventorySlot _selectedInventorySlot;
        private EquipmentSlotTypes _selectedEquipmentSlotType;
        private Action _onDeselect;

        public InventorySlot SelectedInventorySlot => _selectedInventorySlot;
        public EquipmentSlotTypes SelectedEquipmentSlotType => _selectedEquipmentSlotType;
        public Action OnEquipmentSlotSelected;
        public Action OnEquipmentSlotSelectionCleared;

        public void SelectEquipmentSlot(InventorySlot slot, Action onDeselect)
        {
            _onDeselect?.Invoke();
            _selectedInventorySlot = slot;
            _onDeselect = onDeselect;
            OnEquipmentSlotSelected?.Invoke();
        }

        public void SelectEquipmentSlotType(EquipmentSlotTypes slotType)
        {
            _selectedEquipmentSlotType = slotType;
        }

        public void Clear()
        {
            _onDeselect?.Invoke();
            _selectedInventorySlot = null;
            _onDeselect = null;
            OnEquipmentSlotSelectionCleared?.Invoke();
        }
    }
}