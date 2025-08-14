using AdventurerVillage.HexagonGridSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AdventurerVillage.InputControl
{
    public class OnClickController : MonoBehaviour
    {
        [SerializeField] private InputData inputData;
        [SerializeField] private SelectedHexCellData selectedHexCellData;
        [SerializeField] private LayerMask hexCellLayerMask;
        private Coroutine _onMouseClickCoroutine;

        private PlayerActions.MapViewActions MapViewActions => inputData.MapViewActions;

        private void OnClickActionStarted(InputAction.CallbackContext obj)
        {
            if (inputData.IsPointerOverGameObject) return;
            var mousePos = MapViewActions.MosePosition.ReadValue<Vector2>();
            var inputRay = Camera.main.ScreenPointToRay(mousePos);
            if (!Physics.Raycast(inputRay, out var hit, hexCellLayerMask)) return;
            if (!hit.collider.TryGetComponent<HexCellRaycastTarget>(out var targetCell)) return;
            selectedHexCellData.SelectHexCell(targetCell.HexCell);
        }

        private void OnClickActionCancelled(InputAction.CallbackContext obj)
        {
        }

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            MapViewActions.Click.started += OnClickActionStarted;
            MapViewActions.Click.canceled += OnClickActionCancelled;
        }

        private void Start()
        {
            StartCoroutine(inputData.CheckIfPointerOverGameObject());
        }

        private void OnDisable()
        {
            MapViewActions.Click.started -= OnClickActionStarted;
            MapViewActions.Click.canceled -= OnClickActionCancelled;
        }

        #endregion
    }
}