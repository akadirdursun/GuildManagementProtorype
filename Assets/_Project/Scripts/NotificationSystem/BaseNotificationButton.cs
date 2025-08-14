using System;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.NotificationSystem
{
    [RequireComponent(typeof(Button))]
    public abstract class BaseNotificationButton: MonoBehaviour
    {
        [SerializeField] private Button button;
        public Action OnClick;

        protected abstract void OnButtonClicked();
        
        #region MonoBehaviour Methods

        private void OnEnable()
        {
            button.onClick.AddListener(OnButtonClicked);
            button.onClick.AddListener(() => { OnClick?.Invoke();});
        }

        private void OnDisable()
        {
            button.onClick.RemoveAllListeners();
        }

        #endregion
    }
}