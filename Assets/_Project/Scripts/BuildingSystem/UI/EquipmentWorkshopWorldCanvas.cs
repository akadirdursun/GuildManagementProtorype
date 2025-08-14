using System;
using ADK.Common.ObjectPooling;
using AdventurerVillage.CraftingSystem;
using AdventurerVillage.ItemSystem.Equipments;
using AdventurerVillage.TimeSystem;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.BuildingSystem.UI
{
    public class EquipmentWorkshopWorldCanvas : MonoBehaviour
    {
        [SerializeField] private DateData dateData;
        [SerializeField] private EquipmentDatabase equipmentDatabase;
        [SerializeField] private Canvas canvas;
        [SerializeField] private Image iconImage;
        [SerializeField] private Image fillImage;
        [SerializeField] private CraftPointView craftPointViewPrefab;
        [SerializeField] private Transform craftPointViewParent;

        private CraftingInfo _craftingInfo;
        private IObjectPool<CraftPointView> _craftPointViewPool;

        public void Initialize(CraftingInfo craftingInfo)
        {
            UnregisterEvents();
            _craftingInfo = craftingInfo;
            var equipmentData = equipmentDatabase.GetEquipmentData(_craftingInfo.EquipmentTypeId);
            iconImage.sprite = equipmentData.Icon;
            fillImage.fillAmount = 1f;
            UpdateProgress();
            RegisterEvents();
        }

        public void Show()
        {
            canvas.enabled = true;
        }

        public void Hide()
        {
            canvas.enabled = false;
        }

        private void UpdateProgress()
        {
            var progress = (float)_craftingInfo.RemainingTickCount / _craftingInfo.TotalTickCount;
            fillImage.fillAmount = progress;
        }

        private void SpawnCraftingPointView(float addedValue)
        {
            _craftPointViewPool.Get().Initialize(addedValue);
        }

        private void RegisterEvents()
        {
            if (_craftingInfo == null) return;
            _craftingInfo.OnTimeTickChanged += UpdateProgress;
            _craftingInfo.OnCraftingPointAdded += SpawnCraftingPointView;
        }

        private void UnregisterEvents()
        {
            if (_craftingInfo == null) return;
            _craftingInfo.OnTimeTickChanged -= UpdateProgress;
            _craftingInfo.OnCraftingPointAdded -= SpawnCraftingPointView;
        }

        #region MonoBehaviour Methods

        private void Start()
        {
            _craftPointViewPool = new ObjectPool<CraftPointView>(craftPointViewPrefab, 1, OnCreate, OnGet, OnRelease);
        }

        private void OnDestroy()
        {
            UnregisterEvents();
        }

        #endregion

        #region Pool Methods

        private void OnCreate(CraftPointView obj, ObjectPool<CraftPointView> pool)
        {
            obj.transform.SetParent(craftPointViewParent, false);
            obj.OnCompleteAction += () => pool.Release(obj);
            obj.gameObject.SetActive(false);
        }

        private void OnGet(CraftPointView obj)
        {
            obj.gameObject.SetActive(true);
        }

        private void OnRelease(CraftPointView obj)
        {
            obj.gameObject.SetActive(false);
        }

        #endregion
    }
}