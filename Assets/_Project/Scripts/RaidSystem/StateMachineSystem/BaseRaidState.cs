using AdventurerVillage.StateMachineSystem;

namespace AdventurerVillage.RaidSystem.StateMachineSystem
{
    public abstract class BaseRaidState: IState
    {
        #region Constructor

        protected BaseRaidState(RaidInfo raidInfo, StateMachine stateMachine)
        {
            RaidInfo = raidInfo;
            StateMachine = stateMachine;
        }

        #endregion
        
        protected readonly RaidInfo RaidInfo;
        protected readonly StateMachine StateMachine;

        public abstract void Enter();

        public abstract void Execute(float time);

        public abstract void Exit();
    }
}