using AdventurerVillage.StateMachineSystem;

namespace AdventurerVillage.CombatSystem.States
{
    public class EarlyCombatState : BaseCombatState
    {
        public EarlyCombatState(CombatBlackboard blackboard, StateMachine stateMachine) : base(blackboard, stateMachine)
        {
        }

        public override void Enter()
        {
        }

        public override void Execute(float time)
        {
            var attackOrder = GetAttackOrder();
            for (int i = 0; i < attackOrder.Length; i++)
            {
                var attacker = attackOrder[i];
            }
            StateMachine.Next();
        }

        public override void Exit()
        {
        }
    }
}