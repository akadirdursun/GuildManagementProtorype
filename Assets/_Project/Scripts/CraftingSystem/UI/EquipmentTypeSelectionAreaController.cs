using AdventurerVillage.ItemSystem.Equipments;
using UnityEngine;

namespace AdventurerVillage.CraftingSystem.UI
{
    public class EquipmentTypeSelectionAreaController : MonoBehaviour
    {
        [SerializeField] private EquipmentDatabase equipmentDatabase;
        [SerializeField] private EquipmentCraftTypeCard equipmentCraftTypeCardPrefab;
        [SerializeField] private Transform scrollContentParent;
        [SerializeField] private CanvasGroup canvasGroup;

        private void Initialize()
        {
            foreach (var equipmentData in equipmentDatabase.EquipmentDataArray)
            {
                var card = Instantiate(equipmentCraftTypeCardPrefab, scrollContentParent);
                card.Initialize(equipmentData);
            }
        }

        public void Enable()
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
        }

        public void Disable()
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0f;
        }

        #region MonoBehaviour Methods

        private void Start()
        {
            Initialize();
        }

        #endregion
    }
}