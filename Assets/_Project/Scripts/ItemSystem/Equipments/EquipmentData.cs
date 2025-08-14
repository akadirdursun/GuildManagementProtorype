using System.Linq;
using AdventurerVillage.CraftingSystem;
using AdventurerVillage.LevelSystem;
using UnityEngine;

namespace AdventurerVillage.ItemSystem.Equipments
{
    [CreateAssetMenu(fileName = "NewEquipmentData", menuName = "Adventurer Village/Item System/Equipments/Equipment Data")]
    public class EquipmentData : Item
    {
        [SerializeField] protected EquipmentTypes equipmentType;
        [SerializeField] protected CraftingPointData craftingPointData;
        [SerializeField] protected EquipmentMeshInfo mesh;
        [SerializeField] private int craftCost;
        [SerializeField] private int baseStaminaForCraft;
        [SerializeField/*, ListDrawerSettings(ListElementLabelName = "grade")*/] protected EquipmentGradeConfig[] gradeConfigs;

        public EquipmentTypes EquipmentType => equipmentType;
        public int CraftCost => craftCost;
        public int BaseStaminaForCraft => baseStaminaForCraft;
        public EquipmentMeshInfo Mesh => mesh;

        public EquipmentInfo CraftEquipment(float craftingPoint, Grade grade)
        {
            var equipmentConfig = gradeConfigs.First(config => config.Grade == grade);
            var combatEffects = craftingPointData.GetStatEffects(craftingPoint, equipmentConfig);
            return new EquipmentInfo(
                id,
                itemName,
                grade,
                equipmentType,
                equipmentConfig.MaxDurability,
                combatEffects);
        }
    }
}