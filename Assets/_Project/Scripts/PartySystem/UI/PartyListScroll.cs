using ADK.Common.ObjectPooling;
using UnityEngine;

namespace AdventurerVillage.PartySystem.UI
{
    public class PartyListScroll : MonoBehaviour
    {
        [SerializeField/*, ChildGameObjectsOnly*/] private CreatePartyCard createPartyCard;
        [SerializeField/*, AssetsOnly*/] private PartyCard partyCardPrefab;
        [SerializeField] private Transform scrollContent;
        [SerializeField] private int defaultPoolSize = 2;

        private IObjectPool<PartyCard> partyCardPool;

        public void Initialize(PartyInfo[] parties)
        {
            partyCardPool.ReleaseAll();
            foreach (var party in parties)
            {
                partyCardPool.Get().Initialize(party);
            }
            createPartyCard.transform.SetAsLastSibling();
        }

        #region MonoBehaviour Methods

        private void Start()
        {
            partyCardPool = new ObjectPool<PartyCard>(partyCardPrefab, defaultPoolSize, OnCreate, OnGet, OnRelease);
        }

        #endregion

        #region Pool

        private void OnCreate(PartyCard card, ObjectPool<PartyCard> pool)
        {
            card.transform.SetParent(transform, false);
            card.gameObject.SetActive(false);
        }

        private void OnGet(PartyCard card)
        {
            card.transform.SetParent(scrollContent, false);
            card.gameObject.SetActive(true);
        }

        private void OnRelease(PartyCard card)
        {
            card.transform.SetParent(transform, false);
            card.gameObject.SetActive(false);
        }

        #endregion
    }
}