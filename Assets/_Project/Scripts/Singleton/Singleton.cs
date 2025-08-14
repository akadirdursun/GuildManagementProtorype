using UnityEngine;

namespace ADK.Common
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T Instance { get; private set; }
        public static bool IsInstanceExist => Instance != null;

        protected virtual void Awake()
        {
            if (IsInstanceExist)
            {
                Debug.LogError($"Instance of {typeof(T)} already exists.");
                Destroy(this);
                return;
            }

            Instance = (T)this;
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }
    }
}