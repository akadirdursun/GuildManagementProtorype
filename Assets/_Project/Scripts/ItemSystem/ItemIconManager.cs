using System.Collections.Generic;
using ADK.Common;
using AdventurerVillage.ItemSystem.Equipments;
using UnityEngine;

namespace AdventurerVillage.ItemSystem
{
    public class ItemIconManager : Singleton<ItemIconManager>
    {
        [SerializeField] private EquipmentDatabase equipmentDatabase;

        private Dictionary<string, Sprite> _itemIconDictionary = new();

        public Sprite GetEquipmentIcon(string itemId)
        {
            if (_itemIconDictionary.TryGetValue(itemId, out var equipmentIcon))
            {
                return equipmentIcon;
            }

            var equipmentData = equipmentDatabase.GetEquipmentData(itemId);
            equipmentIcon = equipmentData.Icon;
            _itemIconDictionary.Add(itemId, equipmentIcon);
            return equipmentIcon;
        }

        public Sprite GetConsumableItemInfo(string id)
        {
            return null;
        }
    }
}