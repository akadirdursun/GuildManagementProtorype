using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AdventurerVillage.UI.ScreenControlSystems
{
    public abstract class UIScreen : MonoBehaviour
    {
        [SerializeField/*, BoxGroup("UI Screen")*/, Space] protected Canvas canvas;
        [SerializeField/*, BoxGroup("UI Screen")*/] protected Button closeButton;
        [SerializeField/*, BoxGroup("UI Screen")*/] protected bool useUiInputAction = true;
        [SerializeField/*, FoldoutGroup("Event")*/] private UnityEvent onScreenPreShow;
        [SerializeField/*, FoldoutGroup("Event")*/] private UnityEvent onScreenShow;
        [SerializeField/*, FoldoutGroup("Event")*/] private UnityEvent onScreenClose;

        private UIScreenStates _currentState;

        //TODO: Remove these actions if its possible/ No need to unity event and action both at the same time
        public Action OnShow;
        public Action OnClose;
        public bool UseUiInputAction=> useUiInputAction;

        [ContextMenu("Show")]
        internal void Show()
        {
            RegisterToEvents();
            onScreenPreShow?.Invoke();
            OnBeforePopupShow();
            EnableCanvas();
            _currentState = UIScreenStates.Open;
            onScreenShow?.Invoke();
            OnShow?.Invoke();
            OnAfterPopupShow();
        }

        [ContextMenu("Close")]
        internal void Close()
        {
            UnregisterFromEvents();
            onScreenClose?.Invoke();
            OnClose?.Invoke();
            DisableCanvas();
            _currentState = UIScreenStates.Close;
            OnAfterPopupClose();
        }

        internal void MakeVisible()
        {
            if (_currentState != UIScreenStates.Invisible)
            {
                Debug.LogError($"UI Screen ({name}) is not invisible state");
                return;
            }

            EnableCanvas();
            _currentState = UIScreenStates.Open;
        }

        internal virtual void MakeInvisible()
        {
            if (_currentState != UIScreenStates.Open)
            {
                Debug.LogError($"UI Screen ({name}) is not open state");
                return;
            }

            DisableCanvas();
            _currentState = UIScreenStates.Invisible;
        }

        protected virtual void OnCloseButtonClicked()
        {
            UIActionRecorder.Instance.DoPreviousScreenVisible();
        }

        protected virtual void OnAfterPopupClose()
        {
        }

        protected virtual void OnBeforePopupShow()
        {
        }

        protected virtual void OnAfterPopupShow()
        {
        }

        protected virtual void RegisterToEvents()
        {
            closeButton.onClick.AddListener(OnCloseButtonClicked);
        }

        protected virtual void UnregisterFromEvents()
        {
            closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        }

        private IEnumerator OnAfterAwake()
        {
            yield return null;
            DisableCanvas();
            _currentState = UIScreenStates.Close;
        }

        private void EnableCanvas()
        {
            canvas.enabled = true;
        }

        private void DisableCanvas()
        {
            canvas.enabled = false;
        }

        #region State

        private enum UIScreenStates
        {
            Close,
            Open,
            Invisible
        }

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            StartCoroutine(OnAfterAwake());
        }

        #endregion
    }
}