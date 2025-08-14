using ADK.Common;
using ADK.Common.ObjectPooling;
using UnityEngine;

namespace AdventurerVillage.RaidSystem
{
    public class RaidControllerSpawner : Singleton<RaidControllerSpawner>
    {
        [SerializeField] private RaidDatabase raidDatabase;
        [SerializeField] private RaidController raidControllerPrefab;
        [SerializeField] private int defaultPoolSize = 2;
        
        private IObjectPool<RaidController> _raidFigureControllerPool;

        public void SpawnRaidController(RaidInfo raidInfo)
        {
            var raidFigureController = _raidFigureControllerPool.Get();
            raidFigureController.Initialize(raidInfo);
        }

        public void DespawnRaidController(RaidController raidController)
        { 
            _raidFigureControllerPool.Release(raidController);
        }

        public void SpawnSavedRaidControllers()
        {
            foreach (var raidInfo in raidDatabase.Raids)
            {
                SpawnRaidController(raidInfo);
            }
        }

        #region MonoBehaviour Methods

        private void Start()
        {
            _raidFigureControllerPool =
                new ObjectPool<RaidController>(raidControllerPrefab, defaultPoolSize, OnCreate, OnGet, OnRelease);
        }

        #endregion
        
        #region Pool Methods
        
        private void OnCreate(RaidController controller, ObjectPool<RaidController> pool)
        {
            controller.transform.SetParent(transform, false);
            controller.gameObject.SetActive(false);
        }

        private void OnGet(RaidController controller)
        {
            controller.gameObject.SetActive(true);
        }

        private void OnRelease(RaidController controller)
        {
            controller.gameObject.SetActive(false);
        }

        #endregion
    }
}