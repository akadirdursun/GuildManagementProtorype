using System;
using System.Linq;
using UnityEngine;

namespace AdventurerVillage.BuildingSystem
{
    [CreateAssetMenu(fileName = "CraftBuildData", menuName = "Adventurer Village/Building System/Craft Build Data")]
    public class WorkshopBuildingData : BuildingData
    {
        [SerializeField] private CraftBuildingLevelInfo[] craftBuildingLevels;

        public override bool TryToGetBuildingLevelInfo(int level, out BuildingLevelInfo levelInfo)
        {
            var buildingLevelInfoAvailable = craftBuildingLevels.Any(info => info.level == level);
            levelInfo = buildingLevelInfoAvailable ? craftBuildingLevels.First(info => info.level == level) : null;
            return buildingLevelInfoAvailable;
        }

        public CraftBuildingLevelInfo GetCraftBuildingLevelInfo(int level)
        {
            return craftBuildingLevels.First(info => info.level == level);
        }
    }

    [Serializable]
    public class CraftBuildingLevelInfo : BuildingLevelInfo
    {
        [Range(0f, 100f)]public float workShopQualityRate;
    }
}