using System;
using AdventurerVillage.GateSystem;
using AdventurerVillage.PartySystem;
using UnityEngine;

namespace AdventurerVillage.RaidSystem
{
    [CreateAssetMenu(fileName = "RaidData", menuName = "Adventurer Village/Raid System/Raid Data")]
    public class RaidData : ScriptableObject
    {
        private GateInfo _targetGateInfo;
        private PartyInfo _selectedPartyInfo;

        public static Action<GateInfo> OnTargetGateChanged;
        public static Action<PartyInfo> OnPartyChanged;

        public GateInfo TargetGateInfo => _targetGateInfo;
        public PartyInfo SelectedPartyInfo => _selectedPartyInfo;
        public bool IsTargetGateSelected => _targetGateInfo != null;
        public bool IsPartySelected => _selectedPartyInfo != null;

        public void SetGateInfo(GateInfo gateInfo)
        {
            _targetGateInfo = gateInfo;
            OnTargetGateChanged?.Invoke(_targetGateInfo);
        }

        public void SetPartyInfo(PartyInfo partyInfo)
        {
            _selectedPartyInfo = partyInfo;
            OnPartyChanged?.Invoke(_selectedPartyInfo);
        }

        public void ClearRaidData()
        {
            _targetGateInfo = null;
            _selectedPartyInfo = null;
        }

        #region ScriptableObject Methods

        private void Reset()
        {
            ClearRaidData();
        }

        #endregion
    }
}