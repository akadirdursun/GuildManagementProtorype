using AdventurerVillage.CharacterSystem;
using AdventurerVillage.CharacterSystem.UI;
using AdventurerVillage.UI.ScreenControlSystems;
using UnityEngine;

namespace AdventurerVillage.PartySystem.UI
{
    public class PartyDetailScreen : UIScreen
    {
        [SerializeField] private SelectedPartyData selectedPartyData;
        [SerializeField] private CharacterDatabase characterDatabase;
        [SerializeField] private CharacterListScroll freeCharacterListScroll;
        [SerializeField] private CharacterListScroll partyCharacterListScroll;

        private void UpdateView()
        {
            var availableCharacters = characterDatabase.PartyFreeCharacters;
            freeCharacterListScroll.Initialize(availableCharacters);
            var characterNamesInParty = selectedPartyData.SelectedParty.CharacterNames;
            partyCharacterListScroll.Initialize(characterNamesInParty);
        }

        #region UI Screen Methods

        protected override void OnBeforePopupShow()
        {
            base.OnBeforePopupShow();
            UpdateView();
            selectedPartyData.SelectedParty.OnCharacterListChanged += UpdateView;
        }

        protected override void OnAfterPopupClose()
        {
            base.OnAfterPopupClose();
            selectedPartyData.SelectedParty.OnCharacterListChanged -= UpdateView;
        }

        #endregion
    }
}