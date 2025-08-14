using System;
using System.Collections.Generic;
using System.Linq;
using ADK.Common.ObjectPooling;
using AdventurerVillage.CharacterSystem;
using AdventurerVillage.ClassSystem;
using AdventurerVillage.InventorySystem;
using AdventurerVillage.InventorySystem.UI;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.ItemSystem.Equipments.UI
{
    public class EquipmentTypeInventory : MonoBehaviour
    {
        [SerializeField] private SelectedEquipmentSlotData selectedEquipmentSlotData;
        [SerializeField] private InventoryStorage inventoryStorage;
        [SerializeField] private SelectedCharacterData selectedCharacter;
        [SerializeField] private ClassDatabase classDatabase;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private UIInventorySlot inventorySlotPrefab;
        [SerializeField] private Transform slotContentParent;
        [SerializeField] private GameObject noEquipmentAvailableWarning;
        [SerializeField] private Button equipButton;
        [SerializeField] private Button backButton;

        private IObjectPool<UIInventorySlot> _uiInventorySlotPool;

        public void ShowInventory(EquipmentSlotTypes slotType)
        {
            var characterClass =
                classDatabase.GetClassData(selectedCharacter.SelectedCharacterInfo
                    .ClassId) as CombatClass; //TODO: There is no other class than combat class
            if (characterClass == null) return;
            var playerInventory = inventoryStorage.PlayerInventory;
            var equipmentSlotList = new List<InventorySlot>();
            switch (slotType)
            {
                case EquipmentSlotTypes.HeadArmor:
                    equipmentSlotList.AddRange(playerInventory.GetSlotsWithEquipment(slot =>
                        (slot.ItemInfo as EquipmentInfo).EquipmentType == EquipmentTypes.HeadArmor &&
                        characterClass.CanUse(slot.ItemInfo.Id, EquipmentSlotTypes.HeadArmor)));
                    break;
                case EquipmentSlotTypes.BodyArmor:
                    equipmentSlotList.AddRange(playerInventory.GetSlotsWithEquipment(slot =>
                        (slot.ItemInfo as EquipmentInfo).EquipmentType == EquipmentTypes.BodyArmor &&
                        characterClass.CanUse(slot.ItemInfo.Id, EquipmentSlotTypes.BodyArmor)));
                    break;
                case EquipmentSlotTypes.FootArmor:
                    equipmentSlotList.AddRange(playerInventory.GetSlotsWithEquipment(slot =>
                        (slot.ItemInfo as EquipmentInfo).EquipmentType == EquipmentTypes.FootArmor &&
                        characterClass.CanUse(slot.ItemInfo.Id, EquipmentSlotTypes.FootArmor)));
                    break;
                case EquipmentSlotTypes.MainHand:
                    equipmentSlotList.AddRange(playerInventory.GetSlotsWithEquipment(slot =>
                        (slot.ItemInfo as EquipmentInfo).EquipmentType is EquipmentTypes.OneHandedWeapon
                        or EquipmentTypes.DoubleHandedWeapon &&
                        characterClass.CanUse(slot.ItemInfo.Id, EquipmentSlotTypes.MainHand)));
                    break;
                case EquipmentSlotTypes.OffHand:
                    equipmentSlotList.AddRange(playerInventory.GetSlotsWithEquipment(slot =>
                        ((slot.ItemInfo as EquipmentInfo).EquipmentType is EquipmentTypes.Shield or EquipmentTypes.OneHandedWeapon) && characterClass.CanUse(slot.ItemInfo.Id, EquipmentSlotTypes.OffHand)));
                    break;
            }

            _uiInventorySlotPool.ReleaseAll();
            noEquipmentAvailableWarning.SetActive(!equipmentSlotList.Any());
            foreach (var slot in equipmentSlotList)
            {
                _uiInventorySlotPool.Get().Initialize(slot);
            }

            equipButton.interactable = false;
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        public void HideInventory()
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            selectedEquipmentSlotData.Clear();
        }

        private void OnEquipButtonClick()
        {
            var characterInfo = selectedCharacter.SelectedCharacterInfo;
            var equipmentSlot = selectedEquipmentSlotData.SelectedInventorySlot;
            var equipmentInfo = equipmentSlot.ItemInfo as EquipmentInfo;
            equipmentSlot.RemoveItem();
            characterInfo.EquipItem(equipmentInfo, selectedEquipmentSlotData.SelectedEquipmentSlotType, out var oldEquipment);

            if (oldEquipment != null)
                equipmentSlot.AddItem(oldEquipment, out var exceededAmount);

            HideInventory();
        }

        private void OnEquipmentSlotSelected()
        {
            equipButton.interactable = true;
        }

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            equipButton.onClick.AddListener(OnEquipButtonClick);
            backButton.onClick.AddListener(HideInventory);
            selectedEquipmentSlotData.OnEquipmentSlotSelected += OnEquipmentSlotSelected;
        }

        private void Start()
        {
            _uiInventorySlotPool = new ObjectPool<UIInventorySlot>(inventorySlotPrefab, 2,
                CreateSlot, GetSlot, ReleaseSlot);
        }

        private void OnDisable()
        {
            equipButton.onClick.RemoveListener(OnEquipButtonClick);
            backButton.onClick.RemoveListener(HideInventory);
            selectedEquipmentSlotData.OnEquipmentSlotSelected -= OnEquipmentSlotSelected;
        }

        #endregion

        #region Slot Pool

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