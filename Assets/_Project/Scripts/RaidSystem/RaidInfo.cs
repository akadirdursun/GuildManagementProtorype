using System;
using AdventurerVillage.GateSystem;
using AdventurerVillage.HexagonGridSystem;
using AdventurerVillage.PartySystem;
using AdventurerVillage.RaidSystem.StateMachineSystem;

namespace AdventurerVillage.RaidSystem
{
    [Serializable]
    public class RaidInfo
    {
        #region Constructors

        public RaidInfo(){}

    public RaidInfo(PartyInfo partyInfo, GateInfo gateInfo, HexCoordinates currentCoordinate)
        {
            PartyInfo = partyInfo;
            GateInfo = gateInfo;
            CurrentCoordinate = currentCoordinate;
        }

        #endregion
        
        public PartyInfo PartyInfo { get; private set; }
        public GateInfo GateInfo { get; private set; }
        public HexCoordinates CurrentCoordinate { get; private set; }
        public float TimeLeftToMove { get; set; }

        public Action OnRaidMove;
        public Action OnRaidCompleted;
        private RaidStateMachine _raidStateMachine;
        

        public void MoveTo(HexCoordinates coordinates)
        {
            CurrentCoordinate = coordinates;
            OnRaidMove?.Invoke();
        }

        public void StartRaidStateMachine()
        {
            _raidStateMachine = new RaidStateMachine(this);
        }
    }
}