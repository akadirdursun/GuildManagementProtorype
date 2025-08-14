using AdventurerVillage.GuildSystem;
using AdventurerVillage.UI.ScreenControlSystems;
using UnityEngine;

namespace AdventurerVillage.PartySystem.UI
{
    public class PartyListScreen : UIScreen
    {
        [SerializeField] private PlayerGuildData playerGuildData;
        [SerializeField] private PartyListScroll partyListScroll;

        private void SetPartyList()
        {
            var parties = playerGuildData.AllParties;
            partyListScroll.Initialize(parties);
        }

        #region UIScreen Methods

        protected override void RegisterToEvents()
        {
            base.RegisterToEvents();
            playerGuildData.OnPartyListChanged += SetPartyList;
        }

        protected override void UnregisterFromEvents()
        {
            base.UnregisterFromEvents();
            playerGuildData.OnPartyListChanged -= SetPartyList;
        }

        protected override void OnBeforePopupShow()
        {
            base.OnBeforePopupShow();
            SetPartyList();
        }

        #endregion
    }
}