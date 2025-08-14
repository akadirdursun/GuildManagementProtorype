using System.Collections.Generic;
using AdventurerVillage.StateMachineSystem;

namespace AdventurerVillage.RaidSystem.StateMachineSystem
{
    public sealed class RaidStateMachine:StateMachine
    {
        #region Constructors

        public RaidStateMachine(RaidInfo raidInfo)
        {
            _raidInfo = raidInfo;
            Start();
        }

        #endregion
        
        private RaidInfo _raidInfo;
        
        protected override void Start()
        {
            StateOrder = new Queue<IState>();
            StateOrder.Enqueue(new MoveToGateState(_raidInfo,this));
            StateOrder.Enqueue(new MoveBackToCityState(_raidInfo,this));
            Next();
        }

        protected override void Complete()
        {
            _raidInfo.OnRaidCompleted?.Invoke();
        }
    }
}