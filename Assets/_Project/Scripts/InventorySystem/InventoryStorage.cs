using AdventurerVillage.SaveSystem;
using AdventurerVillage.Utilities;
using UnityEngine;

namespace AdventurerVillage.InventorySystem
{
    [CreateAssetMenu(fileName = "InventoryStorage", menuName = "Adventurer Village/Inventory System/Inventory Storage")]
    public class InventoryStorage : SavableScriptableObject
    {
        [SerializeField, ReadOnly] private Inventory playerInventory;

        public Inventory PlayerInventory => playerInventory;

        #region ScriptableObject

        public override void Reset()
        {
            playerInventory = new Inventory();
        }

        #endregion

        #region Save System

        public override void Save()
        {
            //ES3.Save("playerInventory", playerInventory);
        }

        public override void Load()
        {
            //playerInventory = ES3.Load("playerInventory", new Inventory());
        }

        #endregion
    }
}