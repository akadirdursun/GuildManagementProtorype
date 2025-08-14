using System.Linq;
using AdventurerVillage.Utilities;
using UnityEngine;

namespace AdventurerVillage.BuildingSystem
{
    [CreateAssetMenu(fileName = "BuildingDatabase", menuName = "Adventurer Village/Building System/Building Database")]
    public class BuildingDatabase : ScriptableObject
    {
        [SerializeField, Space, ReadOnly] private BuildingData[] buildingData;

        public BuildingData[] GetBuildingDataArray(BuildingData[] excludedBuildings = null)
        {
            return excludedBuildings == null ? buildingData : buildingData.Except(excludedBuildings).ToArray();
        }

        public BuildingData GetBuildingData(string buildingId)
        {
            return buildingData.First(data => data.BuildingID == buildingId);
        }
    }
}