using ADK.Common;
using ADK.Common.ObjectPooling;
using UnityEngine;

namespace AdventurerVillage.NotificationSystem
{
    public abstract class BaseNotificationManager<T, PT> : Singleton<T> where T : Singleton<T> where PT : BaseNotificationButton
    {
        [SerializeField] protected Transform notificationParent;
        [SerializeField] protected PT notificationButtonPref;

        protected IObjectPool<PT> NotificationButtonPool;
        
        #region MonoBehaviour Methods

        protected override void Awake()
        {
            base.Awake();
            NotificationButtonPool = new ObjectPool<PT>(
                notificationButtonPref, 
                1,
                OnCreate,
                OnGet,
                OnRelease);
        }

        #endregion
        
        #region Pool Methods

        private void OnCreate(PT notificationButton, ObjectPool<PT> pool)
        {
            notificationButton.OnClick += () => pool.Release(notificationButton);
            notificationButton.transform.SetParent(transform);
            notificationButton.gameObject.SetActive(false);
        }

        private void OnGet(PT notificationButton)
        {
            notificationButton.transform.SetParent(notificationParent);
            notificationButton.gameObject.SetActive(true);
        }

        private void OnRelease(PT notificationButton)
        {
            notificationButton.gameObject.SetActive(false);
            notificationButton.transform.SetParent(transform);
        }

        #endregion
    }
}