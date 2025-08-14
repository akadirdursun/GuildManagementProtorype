using AdventurerVillage.GuildSystem;
using AdventurerVillage.PartySystem;
using AdventurerVillage.PartySystem.UI;
using AdventurerVillage.RaidSystem.RaidAreaControllers;
using AdventurerVillage.UI.ScreenControlSystems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.RaidSystem.UI
{
    public class RaidStartScreen : UIScreen
    {
        [SerializeField] private RaidData raidData;
        [SerializeField] private RaidDatabase raidDatabase;
        [SerializeField] private PlayerGuildData playerGuildData;
        [SerializeField] private RaidPartyInfoArea raidPartyInfoArea;
        [SerializeField] private PartyListScroll partySelectionListScroll;
        [SerializeField] private TMP_Text gateInfoText;
        [SerializeField] private Button startRaidButton;

        private void OnRaidPartySelected(PartyInfo partyInfo)
        {
            RaidPartyAreaController.Instance.Enable();
            raidPartyInfoArea.gameObject.SetActive(true);
            partySelectionListScroll.gameObject.SetActive(false);
        }

        private void OnChangeRaidPartyButtonClicked()
        {
            RaidPartyAreaController.Instance.Disable();
            raidPartyInfoArea.gameObject.SetActive(false);
            UpdatePartyList();
            partySelectionListScroll.gameObject.SetActive(true);
        }

        private void UpdatePartyList()
        {
            var idleParties = playerGuildData.IdleParties;
            partySelectionListScroll.Initialize(idleParties);
        }

        private void OnStartRaidButtonClicked()
        {
            var raidInfo = raidDatabase.CreateNewRaidInfo();
            RaidControllerSpawner.Instance.SpawnRaidController(raidInfo);
            UIActionRecorder.Instance.ClearAllRecords();
        }

        #region UIScreen Methods

        protected override void RegisterToEvents()
        {
            base.RegisterToEvents();
            RaidData.OnPartyChanged += OnRaidPartySelected;
            playerGuildData.OnPartyListChanged += UpdatePartyList;
            startRaidButton.onClick.AddListener(OnStartRaidButtonClicked);
        }

        protected override void UnregisterFromEvents()
        {
            base.UnregisterFromEvents();
            RaidData.OnPartyChanged -= OnRaidPartySelected;
            playerGuildData.OnPartyListChanged -= UpdatePartyList;
            startRaidButton.onClick.RemoveListener(OnStartRaidButtonClicked);
        }

        protected override void OnBeforePopupShow()
        {
            base.OnBeforePopupShow();
            RaidGateAreaController.Instance.Enable();
            gateInfoText.text = raidData.TargetGateInfo.GetInfoText(true);
            OnChangeRaidPartyButtonClicked();
        }

        protected override void OnAfterPopupClose()
        {
            base.OnAfterPopupClose();
            RaidGateAreaController.Instance.Disable();
            RaidPartyAreaController.Instance.Disable();
            raidPartyInfoArea.gameObject.SetActive(false);
        }

        #endregion

        #region MonoBehaviour Methods

        private void Start()
        {
            raidPartyInfoArea.Initialize(OnChangeRaidPartyButtonClicked);
        }

        #endregion
    }
}