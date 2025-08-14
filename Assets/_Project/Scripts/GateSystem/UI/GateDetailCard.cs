using AdventurerVillage.RaidSystem;
using AdventurerVillage.RaidSystem.UI;
using AdventurerVillage.UI.ScreenControlSystems;
using AKD.Common.GameEventSystem;
using TMPro;
using UnityEngine;

namespace AdventurerVillage.GateSystem.UI
{
    public class GateDetailCard : BaseGateInfoCard
    {
        [SerializeField] private TMP_Text infoText;
        [SerializeField] private Vector3GameEvent focusCameraToPosition;
        protected override void OnSelectButtonClicked()
        {
            UIScreenController.Instance.ShowScreen(typeof(RaidStartScreen));
        }

        protected override void SetView()
        {
            base.SetView();
            infoText.text = _gateInfo.GetInfoText();
            focusCameraToPosition.Invoke(_gateInfo.WorldPosition);
            //TODO: Hide select button if gate state is not idle
        }

        #region MonoBehaviour Methods

        protected override void OnEnable()
        {
            base.OnEnable();
            RaidData.OnTargetGateChanged += Initialize;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            RaidData.OnTargetGateChanged -= Initialize;
        }

        #endregion
    }
}