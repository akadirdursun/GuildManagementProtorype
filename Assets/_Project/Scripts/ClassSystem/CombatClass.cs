using AdventurerVillage.ItemSystem;
using AdventurerVillage.ItemSystem.Equipments;
using UnityEngine;

namespace AdventurerVillage.ClassSystem
{
    //TODO: Remove combat class since there are no other classes
    [CreateAssetMenu(fileName = "CombatClass", menuName = "Adventurer Village/Class System/New Combat Class")]
    public class CombatClass : BaseClass
    {
        [SerializeField] private AvailableEquipmentInfo availableEquipments;
        public bool CanUse(string equipmentID, EquipmentTypes equipmentType)
        {
            return availableEquipments.CanUse(equipmentID, equipmentType);
        }

        public bool CanUse(string equipmentID, EquipmentSlotTypes equipmentSlotType)
        {
            return availableEquipments.CanUse(equipmentID, equipmentSlotType);
        }
    }
}