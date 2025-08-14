using AdventurerVillage.UI.ScreenControlSystems;
using UnityEngine;

namespace AdventurerVillage.InventorySystem.UI
{
    public class InventoryUIScreen : UIScreen
    {
        [SerializeField] private SelectedInventoryData selectedInventoryData;
        [SerializeField] private InventoryViewController inventoryViewController;


        private void InitializeInventoryUIView()
        {
            var selectedInventory = selectedInventoryData.SelectedInventory;
            if (selectedInventory == null) return;
            inventoryViewController.Initialize(selectedInventory);
        }

        #region UIScreen Methods
        
        protected override void OnBeforePopupShow()
        {
            base.OnBeforePopupShow();
            InitializeInventoryUIView();
        }

        protected override void RegisterToEvents()
        {
            base.RegisterToEvents();
            selectedInventoryData.OnSelectedInventoryChanged += InitializeInventoryUIView;
        }

        protected override void UnregisterFromEvents()
        {
            base.UnregisterFromEvents();
            selectedInventoryData.OnSelectedInventoryChanged -= InitializeInventoryUIView;
        }

        #endregion
    }
}