using System;
using AdventurerVillage.ItemSystem;
using AdventurerVillage.Utilities;
using UnityEngine;

namespace AdventurerVillage.InventorySystem
{
    [CreateAssetMenu(fileName = "DragItemData", menuName = "Adventurer Village/Inventory System/Held Item Data")]
    public class DragItemData : ScriptableObject
    {
        #region Enums

        private enum DragState
        {
            EmptyState,
            DragState
        }

        #endregion

        [SerializeField, ReadOnly] private ItemInfo selectedItemInfo;

        private DragState _currentState = DragState.EmptyState;

        public Action<ItemInfo> OnItemSelected;
        public Action OnItemDropped;

        public void OnClickToInventorySlot(InventorySlot inventorySlot)
        {
            switch (_currentState)
            {
                case DragState.EmptyState:
                    OnCurrentStateEmpty(inventorySlot);
                    break;
                case DragState.DragState:
                    OnCurrentStateDrag(inventorySlot);
                    break;
            }
        }

        private void OnCurrentStateEmpty(InventorySlot inventorySlot)
        {
            var removedItem = inventorySlot.RemoveItem();
            if (removedItem == null) return;
            selectedItemInfo = removedItem;
            _currentState = DragState.DragState;
            OnItemSelected?.Invoke(selectedItemInfo);
        }

        private void OnCurrentStateDrag(InventorySlot inventorySlot)
        {
            inventorySlot.ChangeItem(selectedItemInfo, out var replacedItem);
            if (replacedItem != null)
            {
                selectedItemInfo = replacedItem;
                OnItemSelected?.Invoke(selectedItemInfo);
                return;
            }

            selectedItemInfo = null;
            _currentState = DragState.EmptyState;
            OnItemDropped?.Invoke();
        }
    }
}