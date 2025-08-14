using AdventurerVillage.GuildSystem;
using AdventurerVillage.UI.ScreenControlSystems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.PartySystem.UI
{
    public class PartyCard : MonoBehaviour
    {
        [SerializeField] private PlayerGuildData playerGuildData;
        [SerializeField] private SelectedPartyData selectedPartyData;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private Image bannerImage;
        [SerializeField] private Button editButton;
        [SerializeField] private Button removeButton;

        protected PartyInfo PartyInfo;

        public void Initialize(PartyInfo partyInfo)
        {
            PartyInfo = partyInfo;
            titleText.text = PartyInfo.PartyName;
        }

        private void OnEditButtonClicked()
        {
            selectedPartyData.SelectParty(PartyInfo);
            UIScreenController.Instance.ShowScreen(typeof(PartyDetailScreen));
        }

        private void OnRemoveButtonClicked()
        {
            playerGuildData.RemoveParty(PartyInfo);
        }

        #region MonoBehaviour Methods

        protected virtual void OnEnable()
        {
            editButton.onClick.AddListener(OnEditButtonClicked);
            removeButton.onClick.AddListener(OnRemoveButtonClicked);
        }

        protected virtual void OnDisable()
        {
            editButton.onClick.RemoveListener(OnEditButtonClicked);
            removeButton.onClick.RemoveListener(OnRemoveButtonClicked);
        }

        #endregion
    }
}