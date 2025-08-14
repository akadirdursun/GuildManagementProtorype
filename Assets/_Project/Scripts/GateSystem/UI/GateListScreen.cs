using AdventurerVillage.RaidSystem;
using AdventurerVillage.UI.ScreenControlSystems;
using UnityEngine;

namespace AdventurerVillage.GateSystem.UI
{
    public class GateListScreen : UIScreen
    {
        [SerializeField] private SelectedGateData selectedGateData;
        [SerializeField] private GateDatabase gateDatabase;
        [SerializeField] private RaidData raidData;
        [SerializeField] private GateListScroll gateListScroll;

        private void Initialize()
        {
            var gates = gateDatabase.GateInfos;
            gateListScroll.Initialize(gates);
            var selectedGate = selectedGateData.SelectedGate ?? gates[0];
            raidData.SetGateInfo(selectedGate);
        }

        #region UI Screen Methods

        protected override void OnBeforePopupShow()
        {
            base.OnBeforePopupShow();
            Initialize();
        }

        #endregion
    }
}