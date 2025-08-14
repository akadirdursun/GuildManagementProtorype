using UnityEngine;

namespace AdventurerVillage.BuildingSystem
{
    [CreateAssetMenu(fileName = "BuilderCostData", menuName = "Adventurer Village/Building System/Builder Cost Data")]
    public class BuilderCostData : ScriptableObject
    {
        [SerializeField] private BuildingSaveData buildingSaveData;
        [SerializeField] private int builderStartCost;
        [SerializeField] private int builderCostIncreasePerPurchase;

        public int GetCurrentBuilderCost()
        {
            var currentBuilderCost = builderStartCost;
            var buildingAmount = buildingSaveData.TotalClaimedLandCount;
            for (int i = 0; i < buildingAmount; i++)
            {
                currentBuilderCost += i * builderCostIncreasePerPurchase;
            }

            return currentBuilderCost;
        }
    }
}