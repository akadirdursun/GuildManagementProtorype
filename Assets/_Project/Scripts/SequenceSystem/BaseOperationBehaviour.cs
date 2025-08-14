using UnityEngine;
using UnityEngine.Events;

namespace AKD.Common.SequenceSystem
{
    public abstract class BaseOperationBehaviour : MonoBehaviour
    {
        #region Events
        
        public UnityEvent onBegin;
        public UnityEvent onComplete;
        
        #endregion
        
        #region Methods

        public virtual void Init()
        {
            
        }

        internal void BeforeBegin()
        {
            Debug.Log($"Operation before begin {gameObject.name}");
            onBegin?.Invoke();
        }

        public abstract void Begin();
        
        public virtual void Complete()
        {
            Debug.Log($"Operation complete {gameObject.name}");
            onComplete?.Invoke();
        }

        internal virtual void OnKill()
        {
            Debug.Log($"Operation killed {gameObject.name}");
        }

        #endregion
    }
}