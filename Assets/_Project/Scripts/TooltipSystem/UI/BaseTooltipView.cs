using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AdventurerVillage.TooltipSystem.UI
{
    public abstract class BaseTooltipView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected TMP_Text titleText;
        [SerializeField] protected TMP_Text descriptionText;

        protected bool _isFocused;
        protected bool _isLocked;

        public void Lock()
        {
            _isLocked = true;
        }

        public virtual void Unlock()
        {
            _isLocked = false;
        }

        protected virtual void OnTooltipFocused()
        {
        }

        protected virtual void OnTooltipLostFocus()
        {
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isFocused = true;
            Unlock();
            OnTooltipFocused();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isFocused = false;
            if (_isLocked) return;
            //We are waiting for single frame so previous tooltip pointer enter method can be triggered. 
            StartCoroutine(WaitBeforeLostFocus());

            IEnumerator WaitBeforeLostFocus()
            {
                yield return null;
                OnTooltipLostFocus();
            }
        }

        protected virtual void Reset()
        {
            titleText.text = "";
            descriptionText.text = "";
            _isFocused = false;
            _isLocked = false;
        }
    }
}