using UnityEngine;

namespace AdventurerVillage.CraftingSystem
{
    [CreateAssetMenu(fileName = "EquipmentCraftConfig", menuName = "Adventurer Village/Crafting System/Equipment Craft Config")]
    public class EquipmentCraftConfig : ScriptableObject
    {
        [SerializeField] private int tickPerStamina = 3; //How much ticks given for a stamina
        [SerializeField] private int actionPerTick = 1; //when an action triggered
        [SerializeField] private float maxExtraMaterialMultiplier = 2.5f;
        [SerializeField] private float maxProductivityFromExtraMaterial = 20;
        
        public int TickPerStamina => tickPerStamina;
        public int ActionPerTick => actionPerTick;
        public float MaxExtraMaterialMultiplier => maxExtraMaterialMultiplier;
        public float MaxProductivityFromExtraMaterial => maxProductivityFromExtraMaterial;
    }
}