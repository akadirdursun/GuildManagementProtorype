using System.Linq;
using ADK.Common.ObjectPooling;
using AdventurerVillage.BuildingSystem;
using UnityEngine;

namespace AdventurerVillage.CraftingSystem.UI
{
    public class WorkshopSelectionAreaController : MonoBehaviour
    {
        [SerializeField] private BuildingData workshopBuildingData;
        [SerializeField] private BuildingSaveData buildingSaveData;
        [SerializeField] private WorkshopSelectionCard workshopSelectionCardPrefab;
        [SerializeField] private Transform scrollContentParent;
        [SerializeField] private CanvasGroup canvasGroup;
        
        private IObjectPool<WorkshopSelectionCard> _workshopSelectionCardPool;

        public void Initialize()
        {
            var workshops = buildingSaveData.BuildingInfos
                .Where(buildingInfo => buildingInfo.BuildingID == workshopBuildingData.BuildingID &&
                                       buildingInfo.CurrentState == BuildingStates.Idle)
                .OrderByDescending(info => info.BuildingLevel)
                .ToArray();
            
            _workshopSelectionCardPool.ReleaseAll();
            foreach (var workshopInfo in workshops)
            {
                var card = _workshopSelectionCardPool.Get();
                card.Initialize(workshopInfo);
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
            _workshopSelectionCardPool = new ObjectPool<WorkshopSelectionCard>(workshopSelectionCardPrefab, 1, OnCreate, OnGet, OnRelease);
        }

        #endregion

        #region Pool Methods

        private void OnCreate(WorkshopSelectionCard card, ObjectPool<WorkshopSelectionCard> pool)
        {
            card.transform.SetParent(transform, false);
            card.gameObject.SetActive(false);
        }

        private void OnGet(WorkshopSelectionCard card)
        {
            card.transform.SetParent(scrollContentParent,false);
            card.gameObject.SetActive(true);
        }

        private void OnRelease(WorkshopSelectionCard card)
        {
            card.transform.SetParent(transform, false);
            card.gameObject.SetActive(false);
        }

        #endregion
    }
}