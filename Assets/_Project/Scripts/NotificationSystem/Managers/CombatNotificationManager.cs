namespace AdventurerVillage.NotificationSystem
{
    public class CombatNotificationManager : BaseNotificationManager<CombatNotificationManager, CombatReportNotificationButton>
    {
        public void SendNotification(CombatNotificationInfo combatNotificationInfo)
        {
            var notificationButton = NotificationButtonPool.Get();
            notificationButton.Initialize(combatNotificationInfo);
        }
    }
}