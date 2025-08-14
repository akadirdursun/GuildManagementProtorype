using System.Linq;
using UnityEngine;

namespace AdventurerVillage.BuildingSystem
{
    [CreateAssetMenu(fileName = "UtilityBuildingData", menuName = "Adventurer Village/Building System/Utility Build Data")]
    public class UtilityBuildingData : BuildingData
    {
        [SerializeField] private BuildingLevelInfo[] buildingLevels;

        public override bool TryToGetBuildingLevelInfo(int level, out BuildingLevelInfo levelInfo)
        {
            var buildingLevelInfoAvailable = buildingLevels.Any(info => info.level == level);
            levelInfo = buildingLevelInfoAvailable ? buildingLevels.First(info => info.level == level) : null;
            return buildingLevelInfoAvailable;
        }
    }
}