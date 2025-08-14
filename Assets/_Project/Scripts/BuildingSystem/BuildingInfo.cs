using System;
using AdventurerVillage.HexagonGridSystem;

namespace AdventurerVillage.BuildingSystem
{
    [Serializable]
    public class BuildingInfo
    {
        #region Constructors

        public BuildingInfo(string buildingID, HexCoordinates coordinates, int buildingLevel)
        {
            BuildingID = buildingID;
            Coordinates = coordinates;
            BuildingLevel = buildingLevel;
            CurrentState = BuildingStates.Idle;
        }

        #endregion

        public string BuildingID { get; private set; }
        public HexCoordinates Coordinates { get; private set; }
        public int BuildingLevel { get; private set; }
        public BuildingStates CurrentState { get; set; }
    }
}