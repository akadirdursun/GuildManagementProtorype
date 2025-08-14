using System.Collections;
using AdventurerVillage.InputControl;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AdventurerVillage.UI
{
    public class WorldCanvasLookAtCamera : MonoBehaviour
    {
        [SerializeField] private InputData inputData;

        private Transform _cameraTransform;
        private bool _isRotating;

        private void OnRotateStarted(InputAction.CallbackContext context)
        {
            _isRotating = context.ReadValue<float>() != 0f;
        }

        private void OnRotateEnded(InputAction.CallbackContext context)
        {
            _isRotating = false;
        }

        private void LookAtCamera()
        {
            if (_cameraTransform == null) return;
            transform.LookAt(transform.position + _cameraTransform.transform.forward);
        }


        #region MonoBehaviour Methods

        private void OnEnable()
        {
            inputData.MapViewActions.Rotate.performed += OnRotateStarted;
            inputData.MapViewActions.MouseRotation.performed += OnRotateStarted;
            inputData.MapViewActions.MouseRotation.canceled += OnRotateEnded;
        }

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => Camera.main != null);
            _cameraTransform = Camera.main.transform;
            LookAtCamera();
        }

        private void LateUpdate()
        {
            if (!_isRotating) return;
            LookAtCamera();
        }

        private void OnDisable()
        {
            inputData.MapViewActions.Rotate.performed -= OnRotateStarted;
            inputData.MapViewActions.MouseRotation.performed -= OnRotateStarted;
            inputData.MapViewActions.MouseRotation.canceled -= OnRotateEnded;
        }

        #endregion
    }
}