using System.Linq;
using AdventurerVillage.StateMachineSystem;

namespace AdventurerVillage.CombatSystem.States
{
    public class CombatLoopState : BaseCombatState
    {
        public CombatLoopState(CombatBlackboard blackboard, StateMachine stateMachine) : base(blackboard, stateMachine)
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
                attacker.AttackCooldown -= time;
            }

            CharacterCombatConfig[] readyToAttackCharacters;
            do
            {
                readyToAttackCharacters = attackOrder.Where(attacker => attacker.AttackCooldown < 0).ToArray();
                for (int i = 0; i < readyToAttackCharacters.Length; i++)
                {
                    var attacker = readyToAttackCharacters[i];
                }
            } while (readyToAttackCharacters.Any(attacker => attacker.AttackCooldown <= 0f));
            
            if (CombatBlackboard.IsCombatOver())
                StateMachine.Next();
        }

        public override void Exit()
        {
        }
    }
}