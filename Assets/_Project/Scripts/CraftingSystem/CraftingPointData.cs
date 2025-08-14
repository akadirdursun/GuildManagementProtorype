using System;
using System.Collections.Generic;
using System.Linq;
using AdventurerVillage.EffectSystem;
using AdventurerVillage.ItemSystem.Equipments;
using AdventurerVillage.StatSystem;
using AdventurerVillage.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AdventurerVillage.CraftingSystem
{
    [CreateAssetMenu(fileName = "CraftingPointData", menuName = "Adventurer Village/Crafting System/Crafting Point Data")]
    public class CraftingPointData : ScriptableObject
    { [SerializeField/*, ListDrawerSettings(ListElementLabelName = "statType", NumberOfItemsPerPage = 20)*/]
        private StatCraftingPointCostInfo[] statCraftingPointCostInfo;
        
        public StatEffect[] GetStatEffects(float craftingPoint, EquipmentGradeConfig equipmentGradeConfig)
        {
            var combatStatEffectList = new List<StatEffect>();
            var guaranteedStatTypes = equipmentGradeConfig.GuaranteedStatTypes;
            foreach (var effectTypeInfo in guaranteedStatTypes)
            {
                var costInfo = statCraftingPointCostInfo.First(info => info.statType == effectTypeInfo.statType);
                if (costInfo.costPerUnit > craftingPoint) continue;
                var maxAffordableUnitCount = Mathf.FloorToInt(craftingPoint / costInfo.costPerUnit);
                var randomUnitCount = Random.Range(1, maxAffordableUnitCount + 1);
                var totalCost = randomUnitCount * costInfo.costPerUnit;
                var effectValue = randomUnitCount * costInfo.unityQuantity;
                var combatEffect = new StatEffect(effectTypeInfo.statType, effectValue);
                combatStatEffectList.Add(combatEffect);
                craftingPoint -= totalCost;
            }

            while (true)
            {
                var possibleStatTypes = equipmentGradeConfig.GetRandomCombatStatArray();
                if (!HasAffordableEffect(possibleStatTypes, out var affordableEffectTypes))
                {
                    possibleStatTypes = equipmentGradeConfig.GuaranteedStatTypes;
                    if (!HasAffordableEffect(possibleStatTypes, out affordableEffectTypes))
                        break;
                }

                var randomEffectType = affordableEffectTypes.Random();
                var equipmentCombatStat = possibleStatTypes.First(statTypeInfo => statTypeInfo.statType == randomEffectType.statType);
                var maxAffordableUnitCount = Mathf.FloorToInt(craftingPoint / randomEffectType.costPerUnit);
                var randomUnitCount = Random.Range(1, maxAffordableUnitCount + 1);
                var totalCost = randomUnitCount * randomEffectType.costPerUnit;
                var effectValue = randomUnitCount * randomEffectType.unityQuantity;
                var effect = combatStatEffectList.FirstOrDefault(effect => effect.StatType == randomEffectType.statType);
                if (effect != null)
                {
                    effect.IncreaseEffectValue(effectValue);
                }
                else
                {
                    var combatEffect = new StatEffect(equipmentCombatStat.statType, effectValue);
                    combatStatEffectList.Add(combatEffect);
                }


                craftingPoint -= totalCost;
            }

            return combatStatEffectList.ToArray();

            bool HasAffordableEffect(EquipmentCombatStats[] statTypes, out StatCraftingPointCostInfo[] affordableTypes)
            {
                affordableTypes = statCraftingPointCostInfo.Where(info =>
                {
                    var isAffordable = info.costPerUnit <= craftingPoint;
                    if (!isAffordable) return false;
                    var hasStatType =
                        statTypes.Any(statType => statType.statType == info.statType);
                    return hasStatType;
                }).ToArray();

                return affordableTypes.Any();
            }
        }
    }

    [Serializable]
    public struct StatCraftingPointCostInfo
    {
        public StatTypes statType;
        public float unityQuantity;
        public float costPerUnit;
    }
}