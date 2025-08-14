using UnityEngine;

namespace AdventurerVillage.CraftingSystem.UI
{
    public class CraftSelectionAreaController : MonoBehaviour
    {
        [SerializeField] private CraftSelectionData craftSelectionData;
        [SerializeField/*, TabGroup("Selection Area")*/] private WorkshopSelectionAreaController workshopSelectionAreaController;
        [SerializeField/*, TabGroup("Selection Area")*/] private EquipmentTypeSelectionAreaController equipmentTypeSelectionAreaController;
        [SerializeField/*, TabGroup("Selection Area")*/] private CraftsmanSelectionAreaController craftsmanSelectionAreaController;
        [SerializeField/*, TabGroup("Summary View")*/] private SelectedWorkshopCard selectedWorkshopCard;
        [SerializeField/*, TabGroup("Summary View")*/] private SelectedEquipmentCraftTypeCard selectedEquipmentCraftTypeCard;
        [SerializeField/*, TabGroup("Summary View")*/] private SelectedCraftsmanCard selectedCraftsmanCard;
        [SerializeField] private CanvasGroup canvasGroup;

        public void Show()
        {
            HideAllAreas();
            if (!craftSelectionData.IsCraftSelectionOngoing)
            {
                selectedWorkshopCard.Hide();
                selectedEquipmentCraftTypeCard.Hide();
                selectedCraftsmanCard.Hide();
                ShowWorkshopSelectionArea();
            }
            else
            {
                ShowCraftsmanSelectionArea();
            }

            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
        }

        public void Hide()
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0f;
        }

        private void OnWorkshopSelected()
        {
            selectedWorkshopCard.Show();
            workshopSelectionAreaController.Disable();
            ShowEquipmentTypeSelectionArea();
        }

        private void OnEquipmentTypeSelected()
        {
            selectedEquipmentCraftTypeCard.Show();
            equipmentTypeSelectionAreaController.Disable();
            ShowCraftsmanSelectionArea();
        }

        private void OnCraftsmanSelected()
        {
            selectedCraftsmanCard.Show();
        }

        private void ShowWorkshopSelectionArea()
        {
            workshopSelectionAreaController.Initialize();
            workshopSelectionAreaController.Enable();
        }

        private void ShowEquipmentTypeSelectionArea()
        {
            equipmentTypeSelectionAreaController.Enable();
        }

        private void HideAllAreas()
        {
            workshopSelectionAreaController.Disable();
            equipmentTypeSelectionAreaController.Disable();
            craftsmanSelectionAreaController.Disable();
        }

        private void ShowCraftsmanSelectionArea()
        {
            craftsmanSelectionAreaController.Initialize();
            craftsmanSelectionAreaController.Enable();
        }

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            craftSelectionData.OnWorkshopSelected += OnWorkshopSelected;
            craftSelectionData.OnEquipmentTypeSelected += OnEquipmentTypeSelected;
            craftSelectionData.OnCraftsmanSelected += OnCraftsmanSelected;
        }

        private void Start()
        {
            selectedWorkshopCard.Initialize(() =>
            {
                HideAllAreas();
                ShowWorkshopSelectionArea();
            });

            selectedEquipmentCraftTypeCard.Initialize(() =>
            {
                HideAllAreas();
                ShowEquipmentTypeSelectionArea();
            });

            selectedCraftsmanCard.Initialize(() =>
            {
                HideAllAreas();
                ShowCraftsmanSelectionArea();
            });
        }

        private void OnDisable()
        {
            craftSelectionData.OnWorkshopSelected -= OnWorkshopSelected;
            craftSelectionData.OnEquipmentTypeSelected -= OnEquipmentTypeSelected;
            craftSelectionData.OnCraftsmanSelected -= OnCraftsmanSelected;
        }

        #endregion
    }
}