using AdventurerVillage.HexagonGridSystem;
using AdventurerVillage.ResourceSystem;
using AdventurerVillage.UI.ScreenControlSystems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.BuildingSystem.UI
{
    public class PreBuildInfoPanel : MonoBehaviour
    {
        [SerializeField] private SelectedHexCellData selectedHexCellData;
        [SerializeField] private BuildingSaveData buildingSaveData;
        [SerializeField] private ResourceData materialResourceData;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private Button buildButton;

        private BuildingData _buildingData;
        private BuildingLevelInfo _buildingLevelInfo;

        public void Initialize(BuildingData buildingData)
        {
            _buildingData = buildingData;
            titleText.text = _buildingData.BuildingName;
            _buildingData.TryToGetBuildingLevelInfo(1, out _buildingLevelInfo);
            descriptionText.text = $"{_buildingData.BuildingDescription}\n{_buildingLevelInfo.cost} {materialResourceData.ResourceName}";
            buildButton.interactable = materialResourceData.HasAmount(_buildingLevelInfo.cost);
        }

        private void OnBuildButtonClicked()
        {
            if (!materialResourceData.TryToSpendAmount(_buildingLevelInfo.cost)) return;
            var selectedCell = selectedHexCellData.SelectedHexCell;
            var buildingInfo = new BuildingInfo(_buildingData.BuildingID, selectedCell.Coordinates, _buildingLevelInfo.level);
            buildingSaveData.NewBuildingPlaced(buildingInfo);
            BuildingSpawner.Instance.SpawnBuilding(selectedCell, _buildingLevelInfo, buildingInfo);
            UIActionRecorder.Instance.DoPreviousScreenVisible();
        }

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            buildButton.onClick.AddListener(OnBuildButtonClicked);
        }

        private void OnDisable()
        {
            buildButton.onClick.RemoveListener(OnBuildButtonClicked);
        }

        #endregion
    }
}