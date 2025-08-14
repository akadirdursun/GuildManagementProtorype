using System.Collections.Generic;
using AdventurerVillage.InputControl;
using AdventurerVillage.InventorySystem.UI;
using AdventurerVillage.UI.ScreenControlSystems;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

namespace AdventurerVillage.InventorySystem
{
    public class DragDropItemController : MonoBehaviour
    {
        [SerializeField] private InputData inputData;
        [SerializeField] private DragItemData dragItemData;
        [SerializeField] private UiDragItem uiDragItem;
        [SerializeField] private UIScreen uiScreen;
        [SerializeField] private GraphicRaycaster graphicRaycaster;

        private PlayerActions.UIActions UiScreenActions => inputData.UIScreenActions;

        #region MonoBehaviour Methods
        

        private void Start()
        {
            uiDragItem.Initialize(UiScreenActions.Point);
        }

        private void OnEnable()
        {
            uiScreen.OnShow += EnableSlotItemHolder;
            uiScreen.OnClose += DisableSlotItemHolder;
        }

        private void OnDisable()
        {
            uiScreen.OnShow -= EnableSlotItemHolder;
            uiScreen.OnClose -= DisableSlotItemHolder;
        }

        #endregion

        private void EnableSlotItemHolder()
        {
            UiScreenActions.Click.performed += OnMouseClicked;
        }

        private void DisableSlotItemHolder()
        {
            UiScreenActions.Click.performed -= OnMouseClicked;
        }

        private void OnMouseClicked(InputAction.CallbackContext obj)
        {
            if (obj.interaction is not TapInteraction) return;
            var mousePos = UiScreenActions.Point.ReadValue<Vector2>();
            var pointerEventData = new PointerEventData(EventSystem.current)
            {
                position = mousePos
            };
            var raycastResults = new List<RaycastResult>();
            graphicRaycaster.Raycast(pointerEventData, raycastResults);

            foreach (var raycastResult in raycastResults)
            {
                if (!raycastResult.gameObject.TryGetComponent<UIInventorySlot>(out var uiInventorySlot)) continue;

                dragItemData.OnClickToInventorySlot(uiInventorySlot.InventorySlot);
                break;
            }
        }
    }
}