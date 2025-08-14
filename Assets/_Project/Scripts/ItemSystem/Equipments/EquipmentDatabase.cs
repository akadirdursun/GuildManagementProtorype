using System.Linq;
using AdventurerVillage.Utilities;
using UnityEngine;

namespace AdventurerVillage.ItemSystem.Equipments
{
    [CreateAssetMenu(fileName = "EquipmentDatabase", menuName = "Adventurer Village/Item System/Equipments/Equipment Database")]
    public class EquipmentDatabase : ScriptableObject
    {
        [SerializeField, ReadOnly] private EquipmentData[] equipmentDataArray;

        public EquipmentData[] EquipmentDataArray => equipmentDataArray;

        public EquipmentData GetEquipmentData(string craftingInfoEquipmentTypeId)
        {
            return equipmentDataArray.First(data=>data.Id==craftingInfoEquipmentTypeId);
        }
    }
}