using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.BuildingSystem.UI
{
    public class BuildItemCard : MonoBehaviour
    {
        [SerializeField] private Image buildingImage;
        [SerializeField] private Button selectButton;
        [SerializeField] private TMP_Text buildingNameText;
        [SerializeField] private TMP_Text buildingCostText;

        private BuildingData _buildingData;
        private Action _onClickAction;

        public void Initialize(BuildingData buildingData, Action onClickAction)
        {
            _buildingData = buildingData;
            _onClickAction = onClickAction;
            buildingImage.sprite = buildingData.BuildingIcon;
            if (!buildingData.TryToGetBuildingLevelInfo(1, out var buildingLevelInfo))
            {
                buildingCostText.text = "ERROR!!!";
                return;
            }

            buildingNameText.text = buildingData.BuildingName;
            buildingCostText.text = $"<sprite=0> {buildingLevelInfo.cost}";
        }

        private void OnSelectButtonClicked()
        {
            _onClickAction?.Invoke();
        }

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            selectButton.onClick.AddListener(OnSelectButtonClicked);
        }

        private void OnDisable()
        {
            selectButton.onClick.RemoveListener(OnSelectButtonClicked);
        }

        #endregion
    }
}