using AdventurerVillage.CharacterSystem.UI;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.PartySystem.UI
{
    public class FreeCharacterCard : CharacterCard
    {
        [SerializeField] private SelectedPartyData selectedPartyData;
        [SerializeField] private Button addButton;

        private void OnAddButtonClicked()
        {
            selectedPartyData.SelectedParty.AddCharacter(_characterInfo);
        }

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            addButton.onClick.AddListener(OnAddButtonClicked);
        }

        private void OnDisable()
        {
            addButton.onClick.RemoveListener(OnAddButtonClicked);
        }

        #endregion
    }
}