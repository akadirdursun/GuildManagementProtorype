using ADK.Common.ObjectPooling;
using AdventurerVillage.UI.CharacterPanel;
using AdventurerVillage.UI.ScreenControlSystems;
using UnityEngine;

namespace AdventurerVillage.CombatSystem.UI
{
    public class CombatOverviewScreen : UIScreen
    {
        [SerializeField] private CombatPreviewData combatPreviewData;
        [SerializeField] private CharacterCombatView characterCombatViewPrefab;
        [SerializeField] private RectTransform allyCharacterInfoArea;
        [SerializeField] private RectTransform enemyCharacterInfoArea;

        private IObjectPool<CharacterCombatView> characterCombatViewPool;

        #region MonoBehaviour Methods

        private void Start()
        {
            characterCombatViewPool =
                new ObjectPool<CharacterCombatView>(characterCombatViewPrefab, 10, OnCreate, OnGet, OnRelease);
        }

        #endregion
        
        #region UIScreen Methods

        protected override void OnBeforePopupShow()
        {
            base.OnBeforePopupShow();
            UpdateView();
        }

        #endregion

        #region Pool Methods

        private void OnCreate(CharacterCombatView obj, ObjectPool<CharacterCombatView> pool)
        {
            obj.transform.SetParent(transform, false);
            obj.gameObject.SetActive(false);
        }
        
        private void OnGet(CharacterCombatView obj)
        {
            obj.gameObject.SetActive(true);
        }

        private void OnRelease(CharacterCombatView obj)
        {
            obj.transform.SetParent(transform);
            obj.gameObject.SetActive(false);
        }

        #endregion
        
        private void UpdateView()
        {
            characterCombatViewPool.ReleaseAll();
            var combatBlackboard=combatPreviewData.CombatBlackboard;
            var allies = combatBlackboard.Allies;
            var enemies= combatBlackboard.Enemies;
            for (int i = 0; i < allies.Length; i++)
            {
                var characterView = characterCombatViewPool.Get();
                characterView.Initialize(allies[i].CharacterInfo);
                characterView.transform.SetParent(allyCharacterInfoArea, false);
            }

            for (int i = 0; i < enemies.Length; i++)
            {
                var characterView = characterCombatViewPool.Get();
                characterView.Initialize(enemies[i].CharacterInfo);
                characterView.transform.SetParent(enemyCharacterInfoArea, false);
            }
        }
    }
}