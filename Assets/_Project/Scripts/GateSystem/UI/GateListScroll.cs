using ADK.Common.ObjectPooling;
using UnityEngine;

namespace AdventurerVillage.GateSystem.UI
{
    public class GateListScroll : MonoBehaviour
    {
        [SerializeField] private BaseGateInfoCard gateInfoCardPrefab;
        [SerializeField] private Transform contentHolder;
        [SerializeField] private int defaultPoolSize = 2;
        
        private IObjectPool<BaseGateInfoCard> _gateInfoCardPool;

        public void Initialize(GateInfo[] gateInfos)
        {
            _gateInfoCardPool.ReleaseAll();
            foreach (var gateInfo in gateInfos)
            {
                _gateInfoCardPool.Get().Initialize(gateInfo);
            }
        }

        #region MonoBehaviour Methods

        private void Start()
        {
            _gateInfoCardPool =
                new ObjectPool<BaseGateInfoCard>(gateInfoCardPrefab, defaultPoolSize, OnCreate, OnGet, OnRelease);
        }

        #endregion

        #region Pool

        private void OnCreate(BaseGateInfoCard card, ObjectPool<BaseGateInfoCard> pool)
        {
            card.transform.SetParent(transform, false);
            card.gameObject.SetActive(false);
        }

        private void OnGet(BaseGateInfoCard card)
        {
            card.transform.SetParent(contentHolder, false);
            card.gameObject.SetActive(true);
        }

        private void OnRelease(BaseGateInfoCard card)
        {
            card.transform.SetParent(transform, false);
            card.gameObject.SetActive(false);
        }

        #endregion
    }
}