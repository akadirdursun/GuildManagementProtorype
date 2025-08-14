using AdventurerVillage.CharacterSystem.UI;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.PartySystem.UI
{
    public class InPartyCharacterCard : CharacterCard
    {
        [SerializeField] private SelectedPartyData selectedPartyData;
        [SerializeField] private Button removeButton;

        private void OnRemoveButtonClicked()
        {
            selectedPartyData.SelectedParty.RemoveCharacter(_characterInfo);
        }

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            removeButton.onClick.AddListener(OnRemoveButtonClicked);
        }

        private void OnDisable()
        {
            removeButton.onClick.RemoveListener(OnRemoveButtonClicked);
        }

        #endregion
    }
}