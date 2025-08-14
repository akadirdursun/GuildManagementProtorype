using System.Collections.Generic;
using AdventurerVillage.HexagonGridSystem;
using AdventurerVillage.SaveSystem;
using AdventurerVillage.Utilities;
using UnityEngine;

namespace AdventurerVillage.RaidSystem
{
    [CreateAssetMenu(fileName = "RaidDatabase", menuName = "Adventurer Village/Raid System/Raid Database")]
    public class RaidDatabase : SavableScriptableObject
    {
        [SerializeField] private RaidData raidData;
        [SerializeField] private HexMapData hexMapData;
        [SerializeField, ReadOnly] private List<RaidInfo> raids = new();

        public RaidInfo[] Raids => raids.ToArray();

        public RaidInfo CreateNewRaidInfo()
        {
            var newRaidInfo = new RaidInfo(raidData.SelectedPartyInfo, raidData.TargetGateInfo, GameConfig.CityCoordinates);
            AddRaid(newRaidInfo);
            raidData.ClearRaidData();
            return newRaidInfo;
        }

        private void AddRaid(RaidInfo raid)
        {
            raids.Add(raid);
        }

        public void RemoveRaid(RaidInfo raid)
        {
            if (!raids.Contains(raid)) return;
            raids.Remove(raid);
        }

        #region Sava Methods

        public override void Save()
        {
            //ES3.Save("raids", raids);
        }

        public override void Load()
        {
            //raids = ES3.Load("raids", new List<RaidInfo>());
        }

        public override void Reset()
        {
            raids = new();
        }

        #endregion
    }
}