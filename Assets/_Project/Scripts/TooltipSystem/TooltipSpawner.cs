using System.Collections.Generic;
using System.Linq;
using ADK.Common;
using ADK.Common.ObjectPooling;
using AdventurerVillage.ItemSystem;
using AdventurerVillage.NotificationSystem;
using AdventurerVillage.TooltipSystem.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AdventurerVillage.TooltipSystem
{
    public class TooltipSpawner : Singleton<TooltipSpawner>
    {
        [SerializeField] private TMPTooltipKeyData tooltipKeyData;
        [SerializeField] private ItemTooltipView itemTooltipViewPrefab;
        [SerializeField] private Vector2 defaultOffset;
        [SerializeField] private TooltipView tempBasicTooltipViewPrefab;

        private List<BaseTooltipView> _activeTooltipViews = new();

        private IObjectPool<ItemTooltipView> _itemTooltipViewPool;

        private void Start()
        {
            _itemTooltipViewPool = new ObjectPool<ItemTooltipView>(itemTooltipViewPrefab, 1, OnCreate, OnGet, OnRelease);
        }

        //TODO: Send tooltip info class etc.
        public void SpawnTooltipTemp(string tooltip)
        {
            var tooltipView = Instantiate(tempBasicTooltipViewPrefab, transform);
            SetTooltipPosition(tooltipView.transform as RectTransform);
            tooltipView.Initialize(tooltip, "");
            if (_activeTooltipViews.Any())
                _activeTooltipViews.Last().Lock();
            _activeTooltipViews.Add(tooltipView);
        }

        public void SpawnTooltip(ItemInfo itemInfo)
        {
            var title = $"{itemInfo.Name} ({itemInfo.Grade})";
            var effectInfo = itemInfo.ItemEffectInfo;
            var description = itemInfo.ItemDescription;
            var tooltipView = _itemTooltipViewPool.Get();
            tooltipView.Initialize(itemInfo.Icon, title, effectInfo, description);
            if (_activeTooltipViews.Any())
                _activeTooltipViews.Last().Lock();
            _activeTooltipViews.Add(tooltipView);
        }

        public void SpawnTooltip(string key)
        {
            if (!tooltipKeyData.TryToGetTooltipLinkInfo(key, out var tooltipInfo)) return;
            var tooltipView = Instantiate(tooltipInfo.prefab, transform);
            SetTooltipPosition(tooltipView.transform as RectTransform);
            tooltipView.Initialize(tooltipInfo.title, tooltipInfo.description);
            if (_activeTooltipViews.Any())
                _activeTooltipViews.Last().Lock();
            _activeTooltipViews.Add(tooltipView);
        }

        public void SpawnTooltip(int index)
        {
            if (!tooltipKeyData.TryToGetTooltipCharacterInfo(index, out var tooltipInfo)) return;
        }

        public void ReleaseTooltip(ItemTooltipView itemTooltipView)
        {
            if (_activeTooltipViews.Contains(itemTooltipView))
                _activeTooltipViews.Remove(itemTooltipView);

            _itemTooltipViewPool.Release(itemTooltipView);

            if (_activeTooltipViews.Any())
                _activeTooltipViews.Last().Unlock();
        }

        public void ReleaseTooltip(TooltipView tooltipView)
        {
            if (_activeTooltipViews.Contains(tooltipView))
                _activeTooltipViews.Remove(tooltipView);

            Destroy(tooltipView.gameObject);

            if (_activeTooltipViews.Any())
                _activeTooltipViews.Last().Unlock();
        }

        private Vector2 CalculateOffset(Vector2 spawnPos, RectTransform panel)
        {
            var screenSize = new Vector2(Screen.width, Screen.height);
            var corners = new Vector3[4];
            panel.GetWorldCorners(corners);
            var bottomLeftCorner = corners[0];
            var topRightCorner = corners[2];
            var xOffset = 0f;
            CalculateXOffSet();
            var yOffset = 0f;
            CalculateYOffset();


            return new Vector2(xOffset, yOffset);

            void CalculateXOffSet()
            {
                if (bottomLeftCorner.x < 0)
                {
                    xOffset -= bottomLeftCorner.x;
                    return;
                }

                if (topRightCorner.x > screenSize.x)
                {
                    xOffset = screenSize.x - topRightCorner.x;
                }
            }

            void CalculateYOffset()
            {
                if (bottomLeftCorner.y < 0)
                {
                    yOffset -= bottomLeftCorner.y;
                    return;
                }

                if (topRightCorner.y > screenSize.y)
                {
                    yOffset = screenSize.y - topRightCorner.y;
                }
            }
        }

        private void SetTooltipPosition(RectTransform tooltipRectTransform)
        {
            //TODO: Fix double position set
            var mousePos = Mouse.current.position.ReadValue();
            var spawnPos = mousePos + defaultOffset;
            tooltipRectTransform.position = spawnPos;
            spawnPos += CalculateOffset(spawnPos, tooltipRectTransform);
            tooltipRectTransform.position = spawnPos;
        }

        #region Pool Methods

        private void OnCreate<T>(T obj, ObjectPool<T> pool) where T : MonoBehaviour
        {
            obj.transform.SetParent(transform);
            obj.gameObject.SetActive(false);
        }

        private void OnGet<T>(T obj) where T : MonoBehaviour
        {
            SetTooltipPosition(obj.transform as RectTransform);
            obj.gameObject.SetActive(true);
        }

        private void OnRelease<T>(T obj) where T : MonoBehaviour
        {
            obj.gameObject.SetActive(false);
        }

        #endregion
    }
}