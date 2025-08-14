using AdventurerVillage.BuildingSystem;
using AdventurerVillage.ResourceSystem;
using AdventurerVillage.UI.ScreenControlSystems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.HexagonGridSystem.UI
{
    public class ClaimableLandScreen : UIScreen
    {
        [SerializeField] private SelectedHexCellData selectedHexCellData;
        [SerializeField] private BuilderCostData builderCostData;
        [SerializeField] private BuildingSaveData buildingSaveData;
        [SerializeField] private ResourceData materialResourceData;
        [SerializeField] private Button claimButton;
        [SerializeField] private TMP_Text costText;

        private int _cost;
        private void UpdateView()
        {
            _cost = builderCostData.GetCurrentBuilderCost();
            claimButton.interactable = materialResourceData.HasAmount(_cost);
            costText.text = $"{_cost} {materialResourceData.ResourceName}";
        }

        private void OnClaimLandButtonClicked()
        {
            var selectedCell = selectedHexCellData.SelectedHexCell;
            var cost = builderCostData.GetCurrentBuilderCost();
            if (!materialResourceData.TryToSpendAmount(cost)) return;
            buildingSaveData.NewLandClaimed(selectedCell.Coordinates);
            OnCloseButtonClicked();
        }

        private void OnResourceAmountChanged()
        {
            claimButton.interactable = materialResourceData.HasAmount(_cost);
        }

        #region UIScreen methods

        protected override void RegisterToEvents()
        {
            base.RegisterToEvents();
            claimButton.onClick.AddListener(OnClaimLandButtonClicked);
            materialResourceData.OnResourceAmountChanged += OnResourceAmountChanged;
        }

        protected override void UnregisterFromEvents()
        {
            base.UnregisterFromEvents();
            claimButton.onClick.RemoveListener(OnClaimLandButtonClicked);
            materialResourceData.OnResourceAmountChanged -= OnResourceAmountChanged;
        }

        protected override void OnBeforePopupShow()
        {
            base.OnBeforePopupShow();
            UpdateView();
        }

        protected override void OnAfterPopupClose()
        {
            base.OnAfterPopupClose();
            selectedHexCellData.ClearSelectedHexCell();
        }

        #endregion
    }
}