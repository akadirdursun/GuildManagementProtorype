using AdventurerVillage.HexagonGridSystem;
using AdventurerVillage.UI.ScreenControlSystems;
using AKD.Common.GameEventSystem;
using UnityEngine;

namespace AdventurerVillage.BuildingSystem.UI
{
    public class BuildingSelectionScreen : UIScreen
    {
        [Header("Buildings")]
        [SerializeField] private BuildingDatabase buildingDatabase;
        [SerializeField] private BuildItemCard buildItemCardPrefab;
        [SerializeField] private Transform buildingContentParent;
        [SerializeField] private BuildingData[] excludedBuildings;
        [Header("Pre Build Area")]
        [SerializeField] private PreBuildInfoPanel preBuildInfoPanel;
        [SerializeField] private CanvasGroup preBuildingAreaCanvasGroup;
        [SerializeField] private SelectedHexCellData selectedHexCellData;
        [SerializeField] private Vector3GameEvent focusCameraToPosition;

        private void Initialize()
        {
            var buildings = buildingDatabase.GetBuildingDataArray(excludedBuildings);
            foreach (var buildingData in buildings)
            {
                var buildItemCard = Instantiate(buildItemCardPrefab, buildingContentParent);
                buildItemCard.Initialize(buildingData, () => ShowBuildingInfo(buildingData));
            }
        }

        private void ShowBuildingInfo(BuildingData buildingData)
        {
            preBuildInfoPanel.Initialize(buildingData);
            ShowBuildingInfoPanel();
        }

        private void ShowBuildingInfoPanel()
        {
            preBuildingAreaCanvasGroup.alpha = 1f;
            preBuildingAreaCanvasGroup.interactable = true;
        }

        private void HideBuildingInfoPanel()
        {
            preBuildingAreaCanvasGroup.alpha = 0f;
            preBuildingAreaCanvasGroup.interactable = false;
        }

        #region UIScreen Methods

        protected override void OnBeforePopupShow()
        {
            base.OnBeforePopupShow();
            focusCameraToPosition.Invoke(selectedHexCellData.SelectedHexCell.Coordinates.ToWorldPosition());
        }

        protected override void OnAfterPopupClose()
        {
            base.OnAfterPopupClose();
            selectedHexCellData.ClearSelectedHexCell();
            HideBuildingInfoPanel();
        }

        #endregion

        #region MonoBehaviour Methods

        private void Start()
        {
            Initialize();
        }

        #endregion
    }
}