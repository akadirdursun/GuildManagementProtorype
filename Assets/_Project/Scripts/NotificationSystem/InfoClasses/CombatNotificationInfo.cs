using AdventurerVillage.CombatSystem;

namespace AdventurerVillage.NotificationSystem
{
    public class CombatNotificationInfo
    {
        public CombatNotificationInfo(string title, string description, CombatBlackboard combatBlackboard)
        {
            Title = title;
            Description = description;
            CombatBlackboard = combatBlackboard;
        }
        
        public readonly string Title;
        public readonly string Description;
        public readonly CombatBlackboard CombatBlackboard;
    }
}