using AdventurerVillage.UI.ScreenControlSystems;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.CraftingSystem.UI
{
    public class CraftingScreen : UIScreen
    {
        [SerializeField] private CraftSelectionData craftSelectionData;
        [SerializeField] private CraftSelectionAreaController craftSelectionAreaController;
        [SerializeField] private CraftOverviewAreaController craftOverviewAreaController;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button backButton;

        private void Initialize()
        {
            craftOverviewAreaController.Hide();
            craftSelectionAreaController.Show();
            nextButton.interactable = false;
        }

        private void OnCraftsmanSelected()
        {
            nextButton.interactable = true;
        }

        private void OnNextButtonClicked()
        {
            craftSelectionAreaController.Hide();
            craftOverviewAreaController.Show();
        }

        private void OnBackButtonClicked()
        {
            craftOverviewAreaController.Hide();
            craftSelectionAreaController.Show();
        }

        #region UIScreen Methods

        protected override void RegisterToEvents()
        {
            base.RegisterToEvents();
            nextButton.onClick.AddListener(OnNextButtonClicked);
            backButton.onClick.AddListener(OnBackButtonClicked);
            craftSelectionData.OnCraftsmanSelected += OnCraftsmanSelected;
        }

        protected override void UnregisterFromEvents()
        {
            base.UnregisterFromEvents();
            nextButton.onClick.RemoveListener(OnNextButtonClicked);
            backButton.onClick.RemoveListener(OnBackButtonClicked);
            craftSelectionData.OnCraftsmanSelected -= OnCraftsmanSelected;
        }

        protected override void OnBeforePopupShow()
        {
            base.OnBeforePopupShow();
            Initialize();
        }

        protected override void OnAfterPopupClose()
        {
            base.OnAfterPopupClose();
            craftSelectionData.Clear();
        }

        #endregion
    }
}