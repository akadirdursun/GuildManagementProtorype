using AdventurerVillage.GuildSystem;
using AdventurerVillage.UI;
using AdventurerVillage.UI.ScreenControlSystems;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.PartySystem.UI
{
    public class CreatePartyCard : MonoBehaviour
    {
        [SerializeField] private PlayerGuildData playerGuildData;
        [SerializeField] private SelectedPartyData selectedPartyData;
        [SerializeField] private Button createPartyButton;

        private void OnCreatePartyButtonClicked()
        {
            var newPartyInfo = playerGuildData.CreateNewParty();
            selectedPartyData.SelectParty(newPartyInfo);
            //TODO: Hide Party List Screen
            UIScreenController.Instance.ShowScreen(typeof(PartyDetailScreen));
        }

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            createPartyButton.onClick.AddListener(OnCreatePartyButtonClicked);
        }

        private void OnDisable()
        {
            createPartyButton.onClick.RemoveListener(OnCreatePartyButtonClicked);
        }

        #endregion
    }
}