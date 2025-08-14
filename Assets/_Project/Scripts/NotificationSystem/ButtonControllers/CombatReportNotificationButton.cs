using System;
using AdventurerVillage.UI.ScreenControlSystems;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.NotificationSystem
{
    [RequireComponent(typeof(Button))]
    public class CombatReportNotificationButton : BaseNotificationButton
    {
        private CombatNotificationInfo _combatNotificationInfo;

        public void Initialize(CombatNotificationInfo combatNotificationInfo)
        {
            _combatNotificationInfo = combatNotificationInfo;
            PlayAnimation();
        }

        protected override void OnButtonClicked()
        {
            UIScreenController.Instance.ShowCombatReport(_combatNotificationInfo);
        }

        private void PlayAnimation()
        {
        }
    }
}