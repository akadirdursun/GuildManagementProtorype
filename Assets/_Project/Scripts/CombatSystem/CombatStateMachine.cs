using System.Collections.Generic;
using System.Linq;
using AdventurerVillage.CharacterSystem;
using AdventurerVillage.CombatSystem.States;
using AdventurerVillage.NotificationSystem;
using AdventurerVillage.StateMachineSystem;

namespace AdventurerVillage.CombatSystem
{
    public sealed class CombatStateMachine : StateMachine
    {
        #region Constructors

        public CombatStateMachine(CharacterInfo[] characters, CharacterInfo[] enemies):base()
        {
            var characterConfigs = characters.Select(character => new CharacterCombatConfig(character)).ToArray();
            var enemyConfigs = enemies.Select(enemy => new CharacterCombatConfig(enemy)).ToArray();
            _combatBlackboard = new CombatBlackboard(characterConfigs, enemyConfigs);
            Start();
        }

        #endregion


        private CombatBlackboard _combatBlackboard;
        public CombatBlackboard CombatBlackboard => _combatBlackboard;

        protected override void Start()
        {
            StateOrder = new Queue<IState>();
            StateOrder.Enqueue(new EarlyCombatState(_combatBlackboard, this));
            StateOrder.Enqueue(new CombatLoopState(_combatBlackboard, this));
            Next();
        }

        protected override void Complete()
        {
            var title = "Combat Report";
            var description = "";
            var combatNotification = new CombatNotificationInfo(title, description, _combatBlackboard);
            CombatNotificationManager.Instance.SendNotification(combatNotification);
        }
    }
}