using AdventurerVillage.InventorySystem;
using AdventurerVillage.InventorySystem.UI;
using AdventurerVillage.PartySystem;
using UnityEngine;

namespace AdventurerVillage.RaidSystem.UI
{
    public class InventoryAreaController : MonoBehaviour
    {
        [SerializeField] private InventoryStorage inventoryStorage;
        [SerializeField] private InventoryViewController partyInventoryView;
        [SerializeField] private InventoryViewController playerInventoryView;

        public void Initialize()
        {
            playerInventoryView.Initialize(inventoryStorage.PlayerInventory);
            SetPartyInventory(null);
        }

        private void SetPartyInventory(PartyInfo partyInfo)
        {
            if (partyInfo == null)
            {
                partyInventoryView.gameObject.SetActive(false);
                return;
            }

            partyInventoryView.gameObject.SetActive(true);
            partyInventoryView.Initialize(partyInfo.PartyInventory);
        }

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            RaidData.OnPartyChanged += SetPartyInventory;
        }

        private void OnDisable()
        {
            RaidData.OnPartyChanged -= SetPartyInventory;
        }

        #endregion
    }
}