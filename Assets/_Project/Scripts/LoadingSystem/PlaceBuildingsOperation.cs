using AdventurerVillage.BuildingSystem;
using AKD.Common.SequenceSystem;
using UnityEngine;

namespace AdventurerVillage.LoadingSystem
{
    public class PlaceBuildingsOperation : BaseOperationBehaviour
    {
        [SerializeField] private GameLoadTypeData gameLoadTypeData;
        [SerializeField] private BuildingDatabase buildingDatabase;
        [SerializeField] private BuildingSaveData buildingSaveData;
        [SerializeField] private BuildingData baseBuildingData;

        public override void Begin()
        {
            if (gameLoadTypeData.LoadType == GameLoadType.LoadNewGame)
            {
                var cityBuildingInfo = new BuildingInfo(baseBuildingData.BuildingID, GameConfig.CityCoordinates, 1);
                buildingSaveData.NewBuildingPlaced(cityBuildingInfo);
            }

            buildingSaveData.Initialize();
            Complete();
        }
    }
}