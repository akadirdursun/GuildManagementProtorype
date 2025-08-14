using System;
using UnityEngine;

namespace AdventurerVillage.BuildingSystem
{
    [Serializable]
    public class BuildingLevelInfo
    {
        [Range(1, 100)] public int level = 1;
        public BaseBuildingController prefab;
        public int cost;
    }
}