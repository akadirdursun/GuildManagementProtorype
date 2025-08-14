using ADK.Common.ObjectPooling;
using AdventurerVillage.NotificationSystem;
using AdventurerVillage.UI.ScreenControlSystems;
using UnityEngine;

namespace AdventurerVillage.CombatSystem.UI
{
    public class CombatReportScreen : UIScreen
    {
        [SerializeField] private CharacterCombatReportView combatReportViewPrefab;
        [SerializeField] private Transform combatReportViewContainer;

        private IObjectPool<CharacterCombatReportView> _combatViewPool;

        public void Initialize(CombatNotificationInfo combatNotificationInfo)
        {
            _combatViewPool ??= InitCombatViewPool();
            _combatViewPool.ReleaseAll();
            var characters = combatNotificationInfo.CombatBlackboard.Allies;
            foreach (var character in characters)
            {
                _combatViewPool.Get().Initialize(character);
            }
        }

        private ObjectPool<CharacterCombatReportView> InitCombatViewPool()
        {
            return new ObjectPool<CharacterCombatReportView>(combatReportViewPrefab, 4, OnCreate, OnGet, OnRelease);
        }

        #region Pool Methods

        private void OnCreate(CharacterCombatReportView combatReportView, ObjectPool<CharacterCombatReportView> pool)
        {
            combatReportView.transform.SetParent(transform);
            combatReportView.gameObject.SetActive(false);
        }

        private void OnGet(CharacterCombatReportView combatReportView)
        {
            combatReportView.transform.SetParent(combatReportViewContainer);
            combatReportView.gameObject.SetActive(true);
        }

        private void OnRelease(CharacterCombatReportView combatReportView)
        {
            combatReportView.transform.SetParent(transform,false);
            combatReportView.gameObject.SetActive(false);
        }

        #endregion
    }
}