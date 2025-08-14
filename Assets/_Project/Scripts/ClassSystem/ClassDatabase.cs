using System.Collections.Generic;
using System.Linq;
using AdventurerVillage.ItemSystem;
using AdventurerVillage.Utilities;
using UnityEngine;

namespace AdventurerVillage.ClassSystem
{
    [CreateAssetMenu(fileName = "ClassDatabase", menuName = "Adventurer Village/Class System/Class Database")]
    public class ClassDatabase : ScriptableObject
    {
        [SerializeField, ReadOnly] private CombatClass[] combatClasses;

        public CombatClass[] CombatClasses => combatClasses;

        public BaseClass GetClassData(string classId)
        {
            return combatClasses.First(c => c.Id == classId);
        }

        public List<CombatClass> GetCombatClassesCanUse(string equipmentId, EquipmentTypes equipmentType)
        {
            var classes = new List<CombatClass>();
            foreach (var combatClass in combatClasses)
            {
                if (!combatClass.CanUse(equipmentId, equipmentType)) continue;
                classes.Add(combatClass);
            }

            return classes;
        }
    }
}