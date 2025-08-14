using System.Collections.Generic;
using System.Linq;
using AdventurerVillage.InputControl;
using AdventurerVillage.UI.ScreenControlSystems;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

namespace AdventurerVillage.UI
{
    public class DragScreenController : MonoBehaviour
    {
        [SerializeField/*, Required*/] private UIScreen uiScreen;
        [SerializeField] private GraphicRaycaster graphicRaycaster;
        [SerializeField] private InputData inputData;
        private PlayerActions.UIActions UiScreenActions => inputData.UIScreenActions;
        private Vector3 _originalPosition;
        private Vector2 _mousePosition;

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            uiScreen.OnShow += OnScreenEnabled;
            uiScreen.OnClose += OnScreenDisabled;
        }

        private void OnDisable()
        {
            uiScreen.OnShow -= OnScreenEnabled;
            uiScreen.OnClose -= OnScreenDisabled;
        }

        #endregion

        private void OnScreenEnabled()
        {
            _originalPosition = transform.position;
            UiScreenActions.Click.performed += OnClickActionEnter;
            UiScreenActions.Click.canceled += OnClickActionExit;
        }


        private void OnScreenDisabled()
        {
            UiScreenActions.Click.performed -= OnClickActionEnter;
            UiScreenActions.Click.canceled -= OnClickActionExit;
            transform.position = _originalPosition;
        }

        private void OnClickActionEnter(InputAction.CallbackContext obj)
        {
            if (obj.interaction is not HoldInteraction)
                return;
            _mousePosition = UiScreenActions.Point.ReadValue<Vector2>();
            var pointerEventData = new PointerEventData(EventSystem.current)
            {
                position = _mousePosition
            };
            var raycastResults = new List<RaycastResult>();
            graphicRaycaster.Raycast(pointerEventData, raycastResults);
            if (!raycastResults.Any() || !raycastResults[0].gameObject
                    .TryGetComponent<DragScreenController>(out var screenController)) return;

            UiScreenActions.Point.performed += MoveScreen;
        }

        private void MoveScreen(InputAction.CallbackContext obj)
        {
            var newMousePos = UiScreenActions.Point.ReadValue<Vector2>();
            var delta = newMousePos - _mousePosition;
            _mousePosition = newMousePos;
            var pos = transform.position;
            pos.x += delta.x;
            pos.y += delta.y;
            transform.position = pos;
        }

        private void OnClickActionExit(InputAction.CallbackContext obj)
        {
            UiScreenActions.Point.performed -= MoveScreen;
        }
    }
}