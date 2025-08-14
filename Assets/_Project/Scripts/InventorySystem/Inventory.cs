using System;
using System.Collections.Generic;
using System.Linq;
using AdventurerVillage.ItemSystem;
using AdventurerVillage.LevelSystem;
using AdventurerVillage.Utilities;
using UnityEngine;

namespace AdventurerVillage.InventorySystem
{
    [Serializable]
    public class Inventory
    {
        #region Constructors

        public Inventory()
        {
            slots = new List<InventorySlot>();
            for (int i = 0; i < DefaultSlotCount; i++)
            {
                var newSlot = new InventorySlot(i);
                slots.Add(newSlot);
            }
        }

        public Inventory(int slotCount)
        {
            slots = new List<InventorySlot>();
            for (int i = 0; i < slotCount; i++)
            {
                var newSlot = new InventorySlot(i);
                slots.Add(newSlot);
            }
        }

        #endregion

        [SerializeField, ReadOnly] private List<InventorySlot> slots;
        private const int DefaultSlotCount = 30;
        private int CurrentSlotCount => slots.Count;

        public InventorySlot[] Slots => slots.ToArray();
        public Action<InventorySlot> OnSlotAdded;

        public void AddItem(ItemInfo itemInfo)
        {
            bool amountExceeded;
            var itemGrade = itemInfo.Grade;
            var itemAmount = itemInfo.ItemAmount;
            //TODO: Check for stackable first
            if (HasItemInSlot(itemInfo.Id, itemGrade, out var slotsHoldItem))
            {
                foreach (var slot in slotsHoldItem)
                {
                    if (!slot.HasEmptyStackArea) continue;
                    slot.AddItem(itemInfo, out amountExceeded);
                    if (!amountExceeded) return;
                    itemAmount = itemInfo.ItemAmount;
                }
            }

            do
            {
                var emptySlot = slots.First(s => s.IsSlotEmpty);
                emptySlot.AddItem(itemInfo, out amountExceeded);
                CheckForEmptySlot();
                itemAmount = itemInfo.ItemAmount;
            } while (amountExceeded);
        }

        public ItemInfo EmptyItemSlot(int slotId)
        {
            var selectedSlot = slots.FirstOrDefault(s => s.Id == slotId);
            if (selectedSlot == null || selectedSlot.IsSlotEmpty)
            {
                return null;
            }

            return selectedSlot.ItemInfo;
        }

        public InventorySlot[] GetSlotsWithEquipment(Func<InventorySlot, bool> filter)
        {
            var equipmentInfos = new List<InventorySlot>();
            foreach (var slot in slots)
            {
                if (!slot.IsItemEquipment) continue;
                equipmentInfos.Add(slot);
            }

            return equipmentInfos.Where(filter).ToArray();
        }

        private void CheckForEmptySlot()
        {
            if (slots.Any(s => s.IsSlotEmpty)) return;
            var lastSlot = slots.Last();
            var newSlot = new InventorySlot(lastSlot.Id + 1);
            slots.Add(newSlot);
            OnSlotAdded?.Invoke(newSlot);
        }

        private bool HasEmptySlot()
        {
            return slots.Any(s => s.IsSlotEmpty);
        }

        private bool HasItemInSlot(string itemId, Grade itemGrade, out InventorySlot[] slotsHoldItem)
        {
            slotsHoldItem = slots.Where(s => s.HasThisItem(itemId, itemGrade)).ToArray();
            return slotsHoldItem.Any();
        }
    }
}