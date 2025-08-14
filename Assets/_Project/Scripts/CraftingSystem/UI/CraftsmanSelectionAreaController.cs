using System.Linq;
using ADK.Common.ObjectPooling;
using AdventurerVillage.CharacterSystem;
using UnityEngine;

namespace AdventurerVillage.CraftingSystem.UI
{
    public class CraftsmanSelectionAreaController : MonoBehaviour
    {
        [SerializeField] private CharacterDatabase characterDatabase;
        [SerializeField] private CraftsmanCard craftsmanCardPrefab;
        [SerializeField] private Transform scrollContentParent;
        [SerializeField] private CanvasGroup canvasGroup;

        private IObjectPool<CraftsmanCard> _craftsmanCardPool;

        public void Initialize()
        {
            var craftsmanList = characterDatabase.AllCharacters.Where(character =>
                character.CharacterStateController.CurrentState == CharacterStates.IdleState);
            craftsmanList = craftsmanList.OrderByDescending(character => character.Stats.CraftProductivity.Value)
                .ThenByDescending(character => character.Stats.CraftQuality.Value);
            _craftsmanCardPool.ReleaseAll();
            foreach (var character in craftsmanList)
            {
                _craftsmanCardPool.Get().Initialize(character);
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
            _craftsmanCardPool = new ObjectPool<CraftsmanCard>(craftsmanCardPrefab, 0, OnCreate, OnGet, OnRelease);
        }

        #endregion

        #region Pool Methods

        private void OnCreate(CraftsmanCard card, ObjectPool<CraftsmanCard> pool)
        {
            card.transform.SetParent(transform, false);
            card.gameObject.SetActive(false);
        }

        private void OnGet(CraftsmanCard card)
        {
            card.transform.SetParent(scrollContentParent, false);
            card.gameObject.SetActive(true);
        }

        private void OnRelease(CraftsmanCard card)
        {
            card.transform.SetParent(transform, false);
            card.gameObject.SetActive(false);
        }

        #endregion
    }
}