using ADK.Common;
using AdventurerVillage.HexagonGridSystem;
using UnityEngine;

namespace AdventurerVillage.BuildingSystem
{
    public class BuildingSpawner : Singleton<BuildingSpawner>
    {
        [SerializeField] private BuildingDatabase buildingDatabase;
        [SerializeField] private HexMapData hexMapData;

        //TODO: Add a pool
        public void SpawnBuilding(HexCell cell, BuildingLevelInfo levelInfo, BuildingInfo buildingInfo)
        {
            var landManager = cell.HexLandManager;
            var building = Instantiate(levelInfo.prefab);
            building.Initialize(buildingInfo);
            landManager.PlaceBuilding(building);
        }

        public void SpawnBuilding(BuildingInfo buildingInfo)
        {
            var buildingData = buildingDatabase.GetBuildingData(buildingInfo.BuildingID);
            buildingData.TryToGetBuildingLevelInfo(buildingInfo.BuildingLevel, out BuildingLevelInfo buildingLevelInfo);
            hexMapData.TryGetCell(buildingInfo.Coordinates, out HexCell cell);
            SpawnBuilding(cell, buildingLevelInfo, buildingInfo);
        }
    }
}