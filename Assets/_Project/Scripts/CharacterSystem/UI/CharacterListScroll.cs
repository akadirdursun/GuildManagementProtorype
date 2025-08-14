using ADK.Common.ObjectPooling;
using UnityEngine;

namespace AdventurerVillage.CharacterSystem.UI
{
    public class CharacterListScroll : MonoBehaviour
    {
        [SerializeField] private CharacterCard characterCardPrefab;
        [SerializeField] private Transform contentHolder;

        private IObjectPool<CharacterCard> _characterCardPool;

        public void Initialize(string[] characterNames)
        {
            _characterCardPool ??= CreatePool();
            _characterCardPool.ReleaseAll();

            foreach (var characterName in characterNames)
            {
                var characterExist = CharacterInfoService.Instance.TryToGetCharacterInfo(characterName, out var characterInfo);
                if (!characterExist) continue;
                _characterCardPool.Get().Initialize(characterInfo);
            }
        }

        public void Initialize(CharacterInfo[] characterInfos)
        {
            _characterCardPool ??= CreatePool();
            _characterCardPool.ReleaseAll();

            foreach (var characterInfo in characterInfos)
            {
                _characterCardPool.Get().Initialize(characterInfo);
            }
        }

        #region Pool

        private ObjectPool<CharacterCard> CreatePool()
        {
            return new ObjectPool<CharacterCard>(characterCardPrefab, 4, OnCreate, OnGet, OnRelease);
        }

        private void OnCreate(CharacterCard card, ObjectPool<CharacterCard> pool)
        {
            card.transform.SetParent(transform, false);
            card.gameObject.SetActive(false);
        }

        private void OnGet(CharacterCard card)
        {
            card.transform.SetParent(contentHolder, false);
            card.gameObject.SetActive(true);
        }

        private void OnRelease(CharacterCard card)
        {
            card.transform.SetParent(transform, false);
            card.gameObject.SetActive(false);
        }

        #endregion
    }
}