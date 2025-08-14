using AdventurerVillage.ResourceSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.CraftingSystem.UI
{
    public class MaterialCostSettingSlider : MonoBehaviour
    {
        [SerializeField] private EquipmentCraftConfig equipmentCraftConfig;
        [SerializeField] private CraftSelectionData craftSelectionData;
        [SerializeField] private ResourceData materialResourceData;
        [SerializeField] private TMP_Text valueText;
        [SerializeField] private Slider slider;

        public void Initialize()
        {
            slider.minValue = craftSelectionData.SelectedEquipmentData.CraftCost;
            slider.maxValue = craftSelectionData.SelectedEquipmentData.CraftCost * equipmentCraftConfig.MaxExtraMaterialMultiplier;
            slider.value = slider.minValue;
            valueText.text = $"{slider.value}";
        }

        private void OnSliderValueChanged(float value)
        {
            if (materialResourceData.Amount < value)
            {
                slider.value = materialResourceData.Amount;
                return;
            }
            valueText.text = $"{value}";
            var extraMaterialCount = (int)(value - slider.minValue);
            craftSelectionData.ChangeExtraMaterialCount(extraMaterialCount);
        }

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void OnDisable()
        {
            slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }

        #endregion
    }
}