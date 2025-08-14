using AdventurerVillage.BuildingSystem;
using AdventurerVillage.StatSystem.UI;
using AdventurerVillage.TimeSystem;
using AdventurerVillage.UI;
using AdventurerVillage.UI.CharacterPanel;
using TMPro;
using UnityEngine;

namespace AdventurerVillage.CraftingSystem.UI
{
    public class CraftingStatSummaryArea : MonoBehaviour
    {
        [SerializeField] private CraftSelectionData craftSelectionData;
        [SerializeField] private WorkshopBuildingData workshopBuildingData;
        [SerializeField] private DateData dateData;
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private ValueView productivityValueView;
        [SerializeField] private ValueView qualityValueView;
        
        private void SetTimerArea()
        {
            var timeTickCount = craftSelectionData.CraftTimeTickCount;
            var timeFormat = dateData.TickToTimeFormat(timeTickCount);
            timerText.text = $"<sprite=0>{timeFormat}";
        }

        private void SetProductivityValue()
        {
            var productivityFromExtraMaterial = craftSelectionData.ProductivityFromExtraMaterial();
            var craftsmanProductivity = craftSelectionData.SelectedCraftsman.Stats.CraftProductivity.Value;
            productivityValueView.Initialize("Total Productivity:", $"{craftsmanProductivity+productivityFromExtraMaterial:N2}\n" +
                                                                    $"\t• Craftsman: {craftsmanProductivity}\n" +
                                                                    $"\t• Extra Materials: {productivityFromExtraMaterial:N2}");
        }

        private void SetQualityValue()
        {
            var craftsmanQualityRate = craftSelectionData.SelectedCraftsman.Stats.CraftQuality.Value;
            //TODO: Optimize
            var workshopQualityRate = workshopBuildingData.GetCraftBuildingLevelInfo(craftSelectionData.SelectedWorkshop.BuildingLevel)
                .workShopQualityRate;
            qualityValueView.Initialize("Total Quality:", $"{craftsmanQualityRate+workshopQualityRate}%\n" +
                                                          $"\t• Craftsman: {craftsmanQualityRate}%\n" +
                                                          $"\t• Workshop: {workshopQualityRate}%");
        }

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            craftSelectionData.OnStaminaCostChanged += SetTimerArea;
            craftSelectionData.OnCraftSelectionCleared += SetTimerArea;
            craftSelectionData.OnExtraMaterialCostChanged += SetProductivityValue;
            craftSelectionData.OnCraftsmanSelected += SetProductivityValue;
            craftSelectionData.OnCraftsmanSelected += SetQualityValue;
        }

        private void OnDisable()
        {
            craftSelectionData.OnStaminaCostChanged -= SetTimerArea;
            craftSelectionData.OnCraftSelectionCleared -= SetTimerArea;
            craftSelectionData.OnExtraMaterialCostChanged -= SetProductivityValue;
            craftSelectionData.OnCraftsmanSelected -= SetProductivityValue;
            craftSelectionData.OnCraftsmanSelected -= SetQualityValue;
        }

        #endregion
    }
}