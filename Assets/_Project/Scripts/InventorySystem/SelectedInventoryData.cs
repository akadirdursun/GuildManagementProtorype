using System;
using AdventurerVillage.Utilities;
using UnityEngine;

namespace AdventurerVillage.InventorySystem
{
    [CreateAssetMenu(fileName = "SelectedInventoryData",
        menuName = "Adventurer Village/Inventory System/Selected Inventory Data")]
    public class SelectedInventoryData : ScriptableObject
    {
        [SerializeField] private InventoryStorage inventoryStorage;
        [SerializeField, ReadOnly] private Inventory selectedInventory;
        
        public Action OnSelectedInventoryChanged;

        //TODO: Change Later
        public Inventory SelectedInventory => inventoryStorage.PlayerInventory;
        public bool InventorySelected => selectedInventory != null;

        public void SelectInventory(Inventory inventory)
        {
            selectedInventory = inventory;
        }
    }
}