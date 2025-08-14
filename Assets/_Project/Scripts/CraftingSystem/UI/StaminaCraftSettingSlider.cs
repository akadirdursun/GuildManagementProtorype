using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.CraftingSystem.UI
{
    public class StaminaCraftSettingSlider : MonoBehaviour
    {
        [SerializeField] private EquipmentCraftConfig equipmentCraftConfig;
        [SerializeField] private CraftSelectionData craftSelectionData;
        [SerializeField] private TMP_Text valueText;
        [SerializeField] private Slider slider;

        public void Initialize()
        {
            slider.minValue = craftSelectionData.BaseStaminaCost;
            slider.maxValue = craftSelectionData.SelectedCraftsman.Stats.Stamina.CurrentValue;
            slider.value = slider.minValue;
            valueText.text = $"{slider.value}";
        }

        private void OnSliderValueChanged(float value)
        {
            valueText.text = $"{value}";
            craftSelectionData.ChangeStaminaCost((int)value);
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