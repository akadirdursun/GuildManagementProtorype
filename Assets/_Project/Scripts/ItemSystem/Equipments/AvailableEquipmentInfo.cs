using System;
using System.Linq;
using UnityEngine;

namespace AdventurerVillage.ItemSystem.Equipments
{
    [Serializable]
    public class AvailableEquipmentInfo
    {
        [SerializeField] private EquipmentData[] headArmors;
        [SerializeField] private EquipmentData[] bodyArmors;
        [SerializeField] private EquipmentData[] footArmors;
        [SerializeField] private EquipmentData[] mainHandWeapons;
        [SerializeField] private EquipmentData[] offHandWeapons;

        public bool CanUse(string equipmentID, EquipmentTypes equipmentType)
        {
            switch (equipmentType)
            {
                case EquipmentTypes.HeadArmor:
                    return headArmors.Any(data => data.Id == equipmentID);
                case EquipmentTypes.BodyArmor:
                    return bodyArmors.Any(data => data.Id == equipmentID);
                case EquipmentTypes.FootArmor:
                    return footArmors.Any(data => data.Id == equipmentID);
                case EquipmentTypes.Shield:
                    return offHandWeapons.Any(data => data.Id == equipmentID);
                case EquipmentTypes.OneHandedWeapon:
                    return mainHandWeapons.Any(data => data.Id == equipmentID) || offHandWeapons.Any(data => data.Id == equipmentID);
                case EquipmentTypes.DoubleHandedWeapon:
                    return offHandWeapons.Any(data => data.Id == equipmentID) || mainHandWeapons.Any(data => data.Id == equipmentID);
            }

            return false;
        }

        public bool CanUse(string equipmentID, EquipmentSlotTypes equipmentSlotType)
        {
            switch (equipmentSlotType)
            {
                case EquipmentSlotTypes.HeadArmor:
                    return headArmors.Any(data => data.Id == equipmentID);
                case EquipmentSlotTypes.BodyArmor:
                    return bodyArmors.Any(data => data.Id == equipmentID);
                case EquipmentSlotTypes.FootArmor:
                    return footArmors.Any(data => data.Id == equipmentID);
                case EquipmentSlotTypes.MainHand:
                    return mainHandWeapons.Any(data => data.Id == equipmentID);
                case EquipmentSlotTypes.OffHand:
                    return offHandWeapons.Any(data => data.Id == equipmentID);
            }

            return false;
        }
    }
}