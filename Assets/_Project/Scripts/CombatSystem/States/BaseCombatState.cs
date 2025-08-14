using System.Linq;
using AdventurerVillage.StateMachineSystem;

namespace AdventurerVillage.CombatSystem.States
{
    public abstract class BaseCombatState : IState
    {
        protected BaseCombatState(CombatBlackboard blackboard, StateMachine stateMachine)
        {
            CombatBlackboard = blackboard;
            StateMachine = stateMachine;
        }

        protected readonly CombatBlackboard CombatBlackboard;
        protected readonly StateMachine StateMachine;

        public abstract void Enter();
        public abstract void Execute(float time);

        public abstract void Exit();

        public CharacterCombatConfig[] GetAttackOrder()
        {
            var allCharacters = CombatBlackboard.GetAllCharacters();
            return allCharacters.OrderBy(characterConfig => characterConfig.AttackCooldown).ToArray();
        }
    }
}