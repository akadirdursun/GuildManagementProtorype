using System;
using System.Linq;
using AdventurerVillage.BuildingSystem;
using AdventurerVillage.LevelSystem;
using UnityEngine;

namespace AdventurerVillage.AwakenSystems
{
    [CreateAssetMenu(fileName = "CharacterAwakenGradeController",
        menuName = "Adventurer Village/Awaken System/Character Awaken Grade Controller")]
    public class CharacterAwakenGradeController : ScriptableObject
    {
        [SerializeField] private UtilityBuildingData awakenCenterData;
        [SerializeField] private BuildingSaveData buildingSaveData;
        [SerializeField] private AwakenGradePossibilityTables[] awakenGradePossibilityTables;

        public void GetAwakenGrade(out Grade grade, out Grade maxAttributeGrade)
        {
            var awakenCenterInfo = buildingSaveData.GetBuildingInfo(awakenCenterData.BuildingID);
            var possibilityTable = awakenGradePossibilityTables.First(table => awakenCenterInfo.BuildingLevel == table.awakenCenterLevel)
                .gradePossibilityTable;
            possibilityTable.GetRandomGrade(out grade, out maxAttributeGrade);
        }

        #region Structs

        [Serializable]
        public struct AwakenGradePossibilityTables
        {
            public int awakenCenterLevel;
            public GradePossibilityTable gradePossibilityTable;
        }

        #endregion
    }
}