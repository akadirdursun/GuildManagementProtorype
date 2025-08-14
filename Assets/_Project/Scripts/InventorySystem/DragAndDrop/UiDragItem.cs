using AdventurerVillage.ItemSystem;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace AdventurerVillage.InventorySystem
{
    public class UiDragItem : MonoBehaviour
    {
        [SerializeField] private DragItemData dragItemData;
        [SerializeField] private Image iconImage;
        [SerializeField] private TMP_Text stackCountText;
        [SerializeField] private TMP_Text gradeText;

        private InputAction _mousePositionAction;

        #region MonoBehavior Methods

        private void OnEnable()
        {
            if (_mousePositionAction == null) return;
            _mousePositionAction.performed += OnMousePositionChanged;
        }

        private void OnDisable()
        {
            if (_mousePositionAction == null) return;
            _mousePositionAction.performed -= OnMousePositionChanged;
        }

        private void OnDestroy()
        {
            dragItemData.OnItemSelected -= OnItemSelected;
            dragItemData.OnItemDropped -= OnItemDropped;
        }

        #endregion

        public void Initialize(InputAction positionAction)
        {
            _mousePositionAction = positionAction;
            dragItemData.OnItemSelected += OnItemSelected;
            dragItemData.OnItemDropped += OnItemDropped;
            if (gameObject.activeInHierarchy)
                _mousePositionAction.performed += OnMousePositionChanged;
        }

        private void OnItemSelected(ItemInfo itemInfo)
        {
            iconImage.sprite = itemInfo.Icon;
            stackCountText.text = $"{itemInfo.ItemAmount}";
            gradeText.text = $"{itemInfo.Grade}";
            UpdateItemView(itemInfo.IsStackable);
            transform.position = _mousePositionAction.ReadValue<Vector2>();
            gameObject.SetActive(true);
        }

        private void OnItemDropped()
        {
            gameObject.SetActive(false);
        }

        private void UpdateItemView(bool isStackable)
        {
            iconImage.gameObject.SetActive(true);
            stackCountText.gameObject.SetActive(isStackable);
            gradeText.gameObject.SetActive(true);
        }

        private void OnMousePositionChanged(InputAction.CallbackContext obj)
        {
            var position= obj.ReadValue<Vector2>();
            transform.position = new Vector3(position.x, position.y, 0);
        }
    }
}