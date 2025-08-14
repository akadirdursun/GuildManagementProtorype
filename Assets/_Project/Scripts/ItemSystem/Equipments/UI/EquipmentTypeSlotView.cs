using System;
using System.Collections;
using AdventurerVillage.TooltipSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AdventurerVillage.ItemSystem.Equipments.UI
{
    public class EquipmentTypeSlotView : MonoBehaviour, ITooltipDetector
    {
        [SerializeField] private SelectedEquipmentSlotData selectedEquipmentSlotData;
        [SerializeField] private EquipmentSlotTypes equipmentSlotType;
        [SerializeField] private Button button;
        [SerializeField] private Image icon;
        [SerializeField] private Sprite defaultSprite;

        private EquipmentSlot _equipmentSlot;
        private Action<EquipmentSlotTypes> _onSelectAction;

        public EquipmentSlotTypes EquipmentSlotType => equipmentSlotType;

        public void Initialize(Action<EquipmentSlotTypes> onSelectAction)
        {
            _onSelectAction = onSelectAction;
        }

        public void Initialize(EquipmentSlot equipmentSlot)
        {
            UnregisterEquipmentChangeEvent();
            _equipmentSlot = equipmentSlot;
            RegisterEquipmentChangeEvent();
            UpdateView();
        }

        private void UpdateView()
        {
            var sprite = _equipmentSlot.IsEmpty ? defaultSprite : _equipmentSlot.EquipmentInfo.Icon;
            icon.sprite = sprite;
        }

        private void OnButtonClicked()
        {
            selectedEquipmentSlotData.SelectEquipmentSlotType(equipmentSlotType);
            _onSelectAction.Invoke(equipmentSlotType);
        }

        private void RegisterEquipmentChangeEvent()
        {
            if (_equipmentSlot == null) return;
            _equipmentSlot.OnEquipmentChanged += UpdateView;
        }

        private void UnregisterEquipmentChangeEvent()
        {
            if (_equipmentSlot == null) return;
            _equipmentSlot.OnEquipmentChanged -= UpdateView;
        }

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnButtonClicked);
        }

        #endregion

        #region Tooltip

        private Coroutine _delayCountdownCoroutine;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_equipmentSlot.IsEmpty) return;
            _delayCountdownCoroutine = StartCoroutine(DelayCountdown());
        }

        private IEnumerator DelayCountdown()
        {
            yield return new WaitForSeconds(0.5f);
            _delayCountdownCoroutine = null;
            TooltipSpawner.Instance.SpawnTooltip(_equipmentSlot.EquipmentInfo);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_delayCountdownCoroutine == null) return;
            StopCoroutine(_delayCountdownCoroutine);
            _delayCountdownCoroutine = null;
        }

        #endregion
    }
}