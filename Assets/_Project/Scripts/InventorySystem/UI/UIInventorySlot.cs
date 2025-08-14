using System.Collections;
using AdventurerVillage.TooltipSystem;
using AdventurerVillage.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AdventurerVillage.InventorySystem.UI
{
    public class UIInventorySlot : MonoBehaviour, ITooltipDetector
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private TMP_Text stackCountText;
        [SerializeField] private TMP_Text gradeText;
        [SerializeField] private float tooltipDelay = 0.5f;
        [SerializeField, ReadOnly] private InventorySlot inventorySlot;

        private Coroutine _delayCountdownCoroutine;
        public InventorySlot InventorySlot => inventorySlot;

        #region MonoBehaviour Methods

        protected virtual void OnEnable()
        {
            RegisterToInventorySlot();
        }

        protected virtual  void OnDisable()
        {
            UnregisterFromInventorySlot();
        }

        #endregion

        public void Initialize(InventorySlot slotInfo)
        {
            UnregisterFromInventorySlot();
            inventorySlot = slotInfo;
            RegisterToInventorySlot();
            UpdateView();
        }

        public void ResetSlot()
        {
            UnregisterFromInventorySlot();
            inventorySlot = null;
        }

        private void UpdateView()
        {
            if (inventorySlot == null || inventorySlot.IsSlotEmpty)
            {
                DisableSlotItems();
                return;
            }

            iconImage.sprite = inventorySlot.Icon;
            stackCountText.text = $"{inventorySlot.ItemAmount}";
            gradeText.text = $"{inventorySlot.ItemGrade}";
            EnableSlotItems();
        }

        private void DisableSlotItems()
        {
            iconImage.gameObject.SetActive(false);
            stackCountText.gameObject.SetActive(false);
            gradeText.gameObject.SetActive(false);
        }

        private void EnableSlotItems()
        {
            iconImage.gameObject.SetActive(true);
            stackCountText.gameObject.SetActive(inventorySlot.StackableItem);
            gradeText.gameObject.SetActive(true);
        }

        private void RegisterToInventorySlot()
        {
            if (inventorySlot == null) return;
            inventorySlot.OnSlotUpdated += UpdateView;
        }

        private void UnregisterFromInventorySlot()
        {
            if (inventorySlot == null) return;
            inventorySlot.OnSlotUpdated -= UpdateView;
        }

        #region Tooltip

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (inventorySlot.IsSlotEmpty) return;
            _delayCountdownCoroutine = StartCoroutine(DelayCountdown());
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            StopDetection();
        }

        private IEnumerator DelayCountdown()
        {
            yield return new WaitForSeconds(tooltipDelay);
            _delayCountdownCoroutine = null;
            if (!inventorySlot.IsSlotEmpty)
                TooltipSpawner.Instance.SpawnTooltip(inventorySlot.ItemInfo);
        }

        private void StopDetection()
        {
            if (_delayCountdownCoroutine == null) return;
            StopCoroutine(_delayCountdownCoroutine);
            _delayCountdownCoroutine = null;
        }

        #endregion
    }
}