using ADK.Common;
using ADK.Common.ObjectPooling;
using AdventurerVillage.CharacterSystem;
using AdventurerVillage.CharacterSystem.CharacterCustomization;
using UnityEngine;

namespace AdventurerVillage.RaidSystem.RaidAreaControllers
{
    public class RaidPartyAreaController : Singleton<RaidPartyAreaController>
    {
        [SerializeField] private RaidData raidData;
        [SerializeField] private CharacterCustomizationController characterViewPrefab;
        [SerializeField] private Transform characterParent;
        [SerializeField] private int defaultPoolSize = 5;
        [SerializeField] private GameObject[] components;
        [SerializeField] private Vector3 areOfPlacement = new Vector3(10f, 0f, 10f);
        [SerializeField] private float diameter = 2f;
        [SerializeField] private LayerMask raidCharacterLayer;
        private IObjectPool<CharacterCustomizationController> _characterPool;

        public void Enable()
        {
            _characterPool.ReleaseAll();
            var characterNames = raidData.SelectedPartyInfo.CharacterNames;
            foreach (var characterName in characterNames)
            {
                var characterExist = CharacterInfoService.Instance.TryToGetCharacterInfo(characterName, out var character);
                if (!characterExist) continue;
                var characterCustomizationController = _characterPool.Get();
                characterCustomizationController.Initialize(character.CharacterModelInfo);
            }

            foreach (var component in components)
            {
                component.SetActive(true);
            }
        }

        public void Disable()
        {
            foreach (var component in components)
            {
                component.SetActive(false);
            }
        }

        private Vector3 FindEmptySpot()
        {
            var radius = diameter / 2f;
            var currentPos = Vector3.zero;
            var maxAttempts = 100;

            var directions = new[]
            {
                new Vector3(1, 0, 0),
                new Vector3(1, 0, 1).normalized,
                //new Vector3(0, 0, 1),
                new Vector3(-1, 0, 1).normalized,
                new Vector3(-1, 0, 0),
                new Vector3(-1, 0, -1).normalized,
                //new Vector3(0, 0, -1),
                new Vector3(1, 0, -1).normalized
            };

            var results = new Collider[1];
            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                // Check if the current position is empty.
                var worldPosition = characterParent.position + currentPos;
                var size = Physics.OverlapSphereNonAlloc(worldPosition, radius, results, raidCharacterLayer);
                if (size == 0)
                {
                    return currentPos;
                }

                // Choose a random direction from the 8 directions.
                Vector3 randomDir = directions[Random.Range(0, directions.Length)];
                Vector3 newPos = currentPos + randomDir * diameter;

                if (IsWithinArea(newPos))
                {
                    currentPos = newPos;
                }
            }

            Debug.LogWarning("No empty spot found within the maximum number of attempts.");
            return Vector3.zero;
        }

        private bool IsWithinArea(Vector3 pos)
        {
            var areaCenter = Vector3.zero;
            float halfX = areOfPlacement.x / 2f;
            float halfZ = areOfPlacement.z / 2f;
            return (pos.x >= areaCenter.x - halfX && pos.x <= areaCenter.x + halfX) &&
                   (pos.z >= areaCenter.z - halfZ && pos.z <= areaCenter.z + halfZ);
        }

        #region MonoBehaviour Methods

        private void Start()
        {
            _characterPool = new ObjectPool<CharacterCustomizationController>(characterViewPrefab, defaultPoolSize, OnCreate, OnGet, OnRelease);
            Disable();
        }

        #endregion

        #region Pool

        private void OnCreate(CharacterCustomizationController character, ObjectPool<CharacterCustomizationController> pool)
        {
            character.transform.SetParent(characterParent);
            character.gameObject.SetActive(false);
        }

        private void OnGet(CharacterCustomizationController character)
        {
            var position = FindEmptySpot();
            character.transform.localPosition = position;
            character.gameObject.SetActive(true);
        }

        private void OnRelease(CharacterCustomizationController character)
        {
            character.gameObject.SetActive(false);
        }

        #endregion
    }
}