using AdventurerVillage.UI.ScreenControlSystems;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.CraftingSystem.UI
{
    public class CraftOverviewAreaController : MonoBehaviour
    {
        [SerializeField/*, TabGroup("Summary View")*/] private SelectedWorkshopCard selectedWorkshopCard;
        [SerializeField/*, TabGroup("Summary View")*/] private SelectedEquipmentCraftTypeCard selectedEquipmentCraftTypeCard;
        [SerializeField/*, TabGroup("Summary View")*/] private SelectedCraftsmanCard selectedCraftsmanCard;
        [SerializeField/*, TabGroup("Sliders")*/] private StaminaCraftSettingSlider staminaCraftSettingSlider;
        [SerializeField/*, TabGroup("Sliders")*/] private MaterialCostSettingSlider materialCostSettingSlider;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Button backButton;
        [SerializeField] private Button craftButton;

        public void Show()
        {
            selectedWorkshopCard.Show();
            selectedEquipmentCraftTypeCard.Show();
            selectedCraftsmanCard.Show();
            staminaCraftSettingSlider.Initialize();
            materialCostSettingSlider.Initialize();
            backButton.gameObject.SetActive(true);
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
        }

        public void Hide()
        {
            backButton.gameObject.SetActive(false);
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0f;
        }

        private void OnCraftButtonClicked()
        {
            CraftingManager.Instance.StartNewEquipmentCrafting();
            UIActionRecorder.Instance.DoPreviousScreenVisible();
        }

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            craftButton.onClick.AddListener(OnCraftButtonClicked);
        }

        private void OnDisable()
        {
            craftButton.onClick.RemoveListener(OnCraftButtonClicked);
        }

        #endregion
    }
}