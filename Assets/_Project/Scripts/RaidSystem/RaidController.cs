using AdventurerVillage.HexagonGridSystem;
using UnityEngine;

namespace AdventurerVillage.RaidSystem
{
    public class RaidController : MonoBehaviour
    {
        private RaidInfo _raidInfo;

        public void Initialize(RaidInfo raidInfo)
        {
            UnregisterFromEvents();
            _raidInfo = raidInfo;
            MoveToHex();
            RegisterToEvents();
            _raidInfo.StartRaidStateMachine();
        }

        private void MoveToHex()
        {
            transform.position = _raidInfo.CurrentCoordinate.ToWorldPosition();
        }

        private void OnRaidCompleted()
        {
            RaidControllerSpawner.Instance.DespawnRaidController(this);
        }

        private void RegisterToEvents()
        {
            if (_raidInfo == null) return;
            _raidInfo.OnRaidMove += MoveToHex;
            _raidInfo.OnRaidCompleted += OnRaidCompleted;
        }

        private void UnregisterFromEvents()
        {
            if (_raidInfo == null) return;
            _raidInfo.OnRaidMove -= MoveToHex;
            _raidInfo.OnRaidCompleted -= OnRaidCompleted;
        }

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            RegisterToEvents();
        }

        private void OnDisable()
        {
            UnregisterFromEvents();
        }

        #endregion
    }
}