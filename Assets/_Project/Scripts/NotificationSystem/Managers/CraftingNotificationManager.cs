using AdventurerVillage.CraftingSystem;

namespace AdventurerVillage.NotificationSystem
{
    public class CraftingNotificationManager : BaseNotificationManager<CraftingNotificationManager, CraftingNotificationButton>
    {
        public void SendNotification(CraftingInfo craftingInfo)
        {
            var notificationButton = NotificationButtonPool.Get();
            notificationButton.Initialize(craftingInfo);
        }
    }
}