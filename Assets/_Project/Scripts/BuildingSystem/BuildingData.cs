using System;
using AdventurerVillage.Utilities;
using UnityEngine;

namespace AdventurerVillage.BuildingSystem
{
    public abstract class BuildingData : ScriptableObject
    {
        [SerializeField, ReadOnly] private string buildingId;
        [SerializeField] private string buildingName;
        [SerializeField] private Sprite buildingIcon;

        [SerializeField, TextArea] private string buildingDescription;

        public string BuildingID => buildingId;
        public string BuildingName => buildingName;
        public Sprite BuildingIcon => buildingIcon;
        public string BuildingDescription => buildingDescription;

        public abstract bool TryToGetBuildingLevelInfo(int level, out BuildingLevelInfo levelInfo);

        private void Awake()
        {
            if (!string.IsNullOrEmpty(buildingId)) return;
            buildingId = Guid.NewGuid().ToString();
        }
    }
}