using System;
using AdventurerVillage.CharacterSystem.UI;
using AdventurerVillage.PartySystem;
using AdventurerVillage.PartySystem.UI;
using AdventurerVillage.UI.ScreenControlSystems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.RaidSystem.UI
{
    public class RaidPartyInfoArea : MonoBehaviour
    {
        [SerializeField] private RaidData raidData;
        [SerializeField] private SelectedPartyData selectedPartyData;
        [SerializeField] private CharacterListScroll characterListScroll;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private Image bannerImage;
        [SerializeField] private Button editButton;
        [SerializeField] private Button changeButton;
        
        private Action _onChangePartyButtonClick;

        public void Initialize(Action onChangePartyButtonClick)
        {
            _onChangePartyButtonClick = onChangePartyButtonClick;
        }

        private void UpdateView()
        {
            characterListScroll.Initialize(raidData.SelectedPartyInfo.CharacterNames);
        }

        private void OnEditButtonClicked()
        {
            selectedPartyData.SelectParty(raidData.SelectedPartyInfo);
            UIScreenController.Instance.ShowScreen(typeof(PartyDetailScreen));
        }

        private void OnChangePartyButtonClick()
        {
            _onChangePartyButtonClick?.Invoke();
        }

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            UpdateView();
            editButton.onClick.AddListener(OnEditButtonClicked);
            changeButton.onClick.AddListener(OnChangePartyButtonClick);
        }

        private void OnDisable()
        {
            editButton.onClick.RemoveListener(OnEditButtonClicked);
            changeButton.onClick.RemoveListener(OnChangePartyButtonClick);
        }

        #endregion
    }
}