using System;
using UnityEngine;

namespace AdventurerVillage.ItemSystem.Equipments
{
    [Serializable]
    public class EquipmentSlot
    {
        #region Constructor

        public EquipmentSlot(){}
        public EquipmentSlot(EquipmentSlotTypes slotType)
        {
            equipmentSlotType = slotType;
        }

        #endregion

        [SerializeField] private EquipmentInfo equipmentInfo;
        
        public Action OnEquipmentChanged;
        private EquipmentSlotTypes equipmentSlotType;
        public bool IsEmpty => equipmentInfo == null;
        public EquipmentTypes EquipmentType => equipmentInfo.EquipmentType;
        public EquipmentSlotTypes EquipmentSlotType => equipmentSlotType;
        public EquipmentInfo EquipmentInfo => equipmentInfo;

        public void Equip(EquipmentInfo equipment)
        {
            if (equipment == null) return;
            if (!IsEmpty)
                UnEquip();

            equipmentInfo = equipment;
            OnEquipmentChanged?.Invoke();
        }

        public void UnEquip()
        {
            if (IsEmpty) return;
            equipmentInfo = null;
            OnEquipmentChanged?.Invoke();
        }
    }
}