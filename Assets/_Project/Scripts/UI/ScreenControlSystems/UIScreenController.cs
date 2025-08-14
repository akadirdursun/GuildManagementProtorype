using System;
using System.Collections.Generic;
using ADK.Common;
using ADK.Common.ObjectPooling;
using AdventurerVillage.CombatSystem.UI;
using AdventurerVillage.NotificationSystem;
using UnityEngine;

namespace AdventurerVillage.UI.ScreenControlSystems
{
    [RequireComponent(typeof(UIActionRecorder))]
    public class UIScreenController : Singleton<UIScreenController>
    {
        [SerializeField] private UIScreen[] screens;
        [SerializeField] private CombatReportScreen combatReportScreenPrefab;

        private UIActionRecorder _actionRecorder;
        private Dictionary<Type, UIScreen> _screenTypes = new();
        private IObjectPool<CombatReportScreen> _combatReportScreenPool;

        public void ShowScreen(Type screenType)
        {
            var screen = _screenTypes[screenType];
            if (screen == null)
            {
                Debug.LogError($"Screen type {nameof(screenType)} not found!");
                return;
            }

            UIActionRecorder.Instance.DoLastScreenInvisible();
            var action = new UIScreenAction(screen);
            _actionRecorder.Record(action);
        }

        public void ShowCombatReport(CombatNotificationInfo combatNotificationInfo)
        {
            var combatReportScreen = _combatReportScreenPool.Get();
            combatReportScreen.Initialize(combatNotificationInfo);
            combatReportScreen.Show();
        }

        #region MonoBehaviour Methods

        protected override void Awake()
        {
            base.Awake();
            foreach (var uiScreen in screens)
            {
                _screenTypes.TryAdd(uiScreen.GetType(), uiScreen);
            }
        }

        private void Start()
        {
            _actionRecorder = UIActionRecorder.Instance;
            _combatReportScreenPool =
                new ObjectPool<CombatReportScreen>(combatReportScreenPrefab, 1, OnCreate, OnGet, OnRelease);
        }

        #endregion

        #region Pool Methods

        private void OnCreate<T>(T uiScreen, ObjectPool<T> pool) where T : UIScreen
        {
            uiScreen.OnClose += () => pool.Release(uiScreen);
            uiScreen.transform.SetParent(transform);
        }

        private void OnGet<T>(T uiScreen) where T : UIScreen
        {
            uiScreen.Show();
        }

        private void OnRelease<T>(T uiScreen) where T : UIScreen
        {
        }

        #endregion
    }
}