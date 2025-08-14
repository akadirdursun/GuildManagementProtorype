using AdventurerVillage.InventorySystem.UI;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.ItemSystem.Equipments.UI
{
    public class EquipmentSlotView : UIInventorySlot
    {
        [SerializeField] private SelectedEquipmentSlotData selectedEquipmentSlotData;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Button button;
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color selectedColor;

        private void OnButtonClicked()
        {
            OnSelect();
            selectedEquipmentSlotData.SelectEquipmentSlot(InventorySlot, OnDeselect);
        }

        private void OnSelect()
        {
            backgroundImage.color = selectedColor;
        }

        private void OnDeselect()
        {
            backgroundImage.color = defaultColor;
        }

        #region MonoBehaviour Methods

        protected override void OnEnable()
        {
            base.OnEnable();
            button.onClick.AddListener(OnButtonClicked);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            button.onClick.RemoveListener(OnButtonClicked);
            OnDeselect();
        }

        #endregion
    }
}