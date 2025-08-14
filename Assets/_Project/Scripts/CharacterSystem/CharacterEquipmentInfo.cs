using System;
using System.Linq;
using AdventurerVillage.ItemSystem;
using AdventurerVillage.ItemSystem.Equipments;
using UnityEngine;

namespace AdventurerVillage.CharacterSystem
{
    [Serializable]
    public class CharacterEquipmentInfo
    {
        #region Constructor

        public CharacterEquipmentInfo()
        {
            equipmentSlots = new[]
            {
                new EquipmentSlot(EquipmentSlotTypes.HeadArmor),
                new EquipmentSlot(EquipmentSlotTypes.BodyArmor),
                new EquipmentSlot(EquipmentSlotTypes.FootArmor),
                new EquipmentSlot(EquipmentSlotTypes.MainHand),
                new EquipmentSlot(EquipmentSlotTypes.OffHand)
            };
        }

        #endregion

        [SerializeField] private EquipmentSlot[] equipmentSlots;

        public EquipmentSlot[] EquipmentSlots => equipmentSlots;
        public EquipmentSlot HeadEquipmentSlot => equipmentSlots.First(slot => slot.EquipmentSlotType == EquipmentSlotTypes.HeadArmor);
        public EquipmentSlot BodyArmorSlot => equipmentSlots.First(slot => slot.EquipmentSlotType == EquipmentSlotTypes.BodyArmor);
        public EquipmentSlot FootArmorSlot => equipmentSlots.First(slot => slot.EquipmentSlotType == EquipmentSlotTypes.FootArmor);
        public EquipmentSlot MainHandSlot => equipmentSlots.First(slot => slot.EquipmentSlotType == EquipmentSlotTypes.MainHand);
        public EquipmentSlot OffHandSlot => equipmentSlots.First(slot => slot.EquipmentSlotType == EquipmentSlotTypes.OffHand);
    }
}