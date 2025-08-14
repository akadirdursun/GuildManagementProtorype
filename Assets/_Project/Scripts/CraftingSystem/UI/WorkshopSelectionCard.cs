using AdventurerVillage.BuildingSystem;
using AdventurerVillage.UI.CharacterPanel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.CraftingSystem.UI
{
    public class WorkshopSelectionCard : MonoBehaviour
    {
        [SerializeField] protected CraftSelectionData craftSelectionData;
        [SerializeField] private BuildingData workshopBuildingData;
        [SerializeField] private BaseStatValueView qualityStatView;
        [SerializeField] private Image buildingImage;
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private Button selectButton;

        private BuildingInfo _buildingInfo;

        public void Initialize(BuildingInfo buildingInfo)
        {
            _buildingInfo = buildingInfo;
            buildingImage.sprite = workshopBuildingData.BuildingIcon;
            levelText.text = $"Lv.{_buildingInfo.BuildingLevel}";
            workshopBuildingData.TryToGetBuildingLevelInfo(buildingInfo.BuildingLevel, out var levelInfo);
            qualityStatView.Initialize(((CraftBuildingLevelInfo)levelInfo).workShopQualityRate);
        }

        protected virtual void OnSelectButtonClick()
        {
            craftSelectionData.SelectWorkshop(_buildingInfo);
        }

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            selectButton.onClick.AddListener(OnSelectButtonClick);
        }

        private void OnDisable()
        {
            selectButton.onClick.RemoveListener(OnSelectButtonClick);
        }

        #endregion
    }
}