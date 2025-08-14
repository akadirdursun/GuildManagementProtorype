using System;
using UnityEngine;
using AdventurerVillage.ItemSystem;
using AdventurerVillage.ItemSystem.Equipments;
using AdventurerVillage.LevelSystem;

namespace AdventurerVillage.InventorySystem
{
    [Serializable/*, ReadOnly*/]
    public class InventorySlot
    {
        public InventorySlot(int id)
        {
            this.id = id;
        }

        //TODO: Completely integrate durability data
        [SerializeField] private int id;
        [SerializeField] private ItemInfo itemInfo;
        [SerializeField] private int itemAmount;
        [SerializeField] private Grade itemGrade;
        public bool IsSlotEmpty => itemInfo == null;
        public bool HasEmptyStackArea => EmptyStackAmount > 0;
        public bool StackableItem => itemInfo is { IsStackable: true };
        public bool IsItemEquipment => itemInfo is EquipmentInfo;
        public bool IsItemConsumable => itemInfo is ConsumableItemInfo;
        private int MaxStackSize => itemInfo.MaxStackSize;
        private int EmptyStackAmount => MaxStackSize - itemAmount;
        public int Id => id;
        public Sprite Icon => itemInfo.Icon;
        public int ItemAmount => itemAmount;
        public Grade ItemGrade => itemGrade;

        public Action OnSlotUpdated;

        public ItemInfo ItemInfo => itemInfo;

        public bool HasThisItem(string itemId, Grade grade)
        {
            return StackableItem && !IsSlotEmpty && itemId == itemInfo.Id && grade == itemGrade;
        }

        public void AddItem(ItemInfo newItemInfo, out bool amountExceeded)
        {
            var grade=newItemInfo.Grade;
            var amount = newItemInfo.ItemAmount;
            if (!HasThisItem(newItemInfo.Id, grade))
            {
                itemInfo = newItemInfo;
                itemGrade = grade;
                amountExceeded = false;
            }
            else
            {
                itemAmount += amount;
                var excessAmount = Mathf.Clamp(itemAmount - MaxStackSize, 0, int.MaxValue);
                amountExceeded = excessAmount > 0;
                itemAmount -= excessAmount;
                newItemInfo.SetItemAmount(excessAmount);
            }

            OnSlotUpdated?.Invoke();
        }

        public void ChangeItem(ItemInfo newItemInfo,  out ItemInfo replacedItem)
        {
            replacedItem = RemoveItem();
            itemInfo = newItemInfo;
            itemGrade = newItemInfo.Grade;
            itemAmount += newItemInfo.ItemAmount;
            OnSlotUpdated?.Invoke();
        }


        public ItemInfo RemoveItem()
        {
            if (IsSlotEmpty) return null;

            var removedItemInfo = ItemInfo;
            ClearInventorySlot();
            return removedItemInfo;
        }

        private void ClearInventorySlot()
        {
            itemInfo = null;
            itemAmount = 0;
            OnSlotUpdated?.Invoke();
        }
    }
}