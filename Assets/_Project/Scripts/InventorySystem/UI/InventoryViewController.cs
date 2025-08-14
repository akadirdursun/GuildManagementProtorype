using ADK.Common.ObjectPooling;
using UnityEngine;

namespace AdventurerVillage.InventorySystem.UI
{
    public class InventoryViewController : MonoBehaviour
    {
        [SerializeField] private UIInventorySlot uiInventorySlotPrefab;
        [SerializeField] private RectTransform slotContentParent;
        [SerializeField] private int defaultPoolSize = 15;

        private Inventory _inventory;
        private IObjectPool<UIInventorySlot> uiInventorySlotPool;

        public void Initialize(Inventory inventory)
        {
            uiInventorySlotPool ??= CreatePool();
            if (_inventory != null)
            {
                _inventory.OnSlotAdded -= AddInventorySlot;
            }

            _inventory = inventory;
            _inventory.OnSlotAdded += AddInventorySlot;
            DrawInventory();
        }

        private void DrawInventory()
        {
            uiInventorySlotPool.ReleaseAll();
            foreach (var inventorySlot in _inventory.Slots)
            {
                AddInventorySlot(inventorySlot);
            }
        }

        private void AddInventorySlot(InventorySlot inventorySlot)
        {
            uiInventorySlotPool.Get().Initialize(inventorySlot);
        }

        #region Slot Pool

        private ObjectPool<UIInventorySlot> CreatePool()
        {
            return new ObjectPool<UIInventorySlot>(uiInventorySlotPrefab, defaultPoolSize,
                CreateSlot, GetSlot, ReleaseSlot);
        }

        private void CreateSlot(UIInventorySlot uiInventorySlot, ObjectPool<UIInventorySlot> pool)
        {
            uiInventorySlot.transform.SetParent(transform, false);
            uiInventorySlot.gameObject.SetActive(false);
        }

        private void GetSlot(UIInventorySlot uiInventorySlot)
        {
            uiInventorySlot.transform.SetParent(slotContentParent, false);
            uiInventorySlot.gameObject.SetActive(true);
        }

        private void ReleaseSlot(UIInventorySlot uiInventorySlot)
        {
            uiInventorySlot.ResetSlot();
            uiInventorySlot.gameObject.SetActive(false);
            uiInventorySlot.transform.SetParent(transform, false);
        }

        #endregion
    }
}