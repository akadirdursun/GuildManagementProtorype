using AdventurerVillage.RaidSystem;
using UnityEngine;

namespace AdventurerVillage.GateSystem.UI
{
    public class GateInfoCard : BaseGateInfoCard
    {
        [SerializeField] private RaidData raidData;

        protected override void OnSelectButtonClicked()
        {
            raidData.SetGateInfo(_gateInfo);
        }
    }
}