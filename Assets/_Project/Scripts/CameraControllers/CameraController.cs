using System.Collections;
using AdventurerVillage.InputControl;
using AdventurerVillage.Utilities;
using PrimeTween;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AdventurerVillage.CameraControllers
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private InputData inputData;
        [SerializeField] private Transform cameraTransform;
        [Header("Horizontal Movement")]
        [SerializeField, ReadOnly] private float currentSpeed;
        [SerializeField] private float maxSpeed = 5f;
        [SerializeField] private float acceleration = 10f; // How quickly camera movement speed up
        [SerializeField] private float damping = 15f; // How quickly camera movement speed down
        [Header("Vertical Movement")]
        [SerializeField] private float stepSize = 2f;
        [SerializeField] private float zoomDampening = 7.5f;
        [SerializeField] private float minHeight = 5f;
        [SerializeField] private float maxHeight = 50f;
        [SerializeField] private float zoomSpeed = 2f;
        [SerializeField, Range(0f, 1f)] private float distanceChangeOnZoom = 0.75f;
        [Header("Rotation")]
        [SerializeField] private float maxRotationSpeed = 1f;
        [Header("Screen Edge Motion")]
        [SerializeField] private bool useScreenEdge = true;
        [SerializeField, Range(0f, 0.1f)] private float edgeTolerance = 0.05f;
        [Header("Focus Settings")]
        [SerializeField] private float focusHeight = 25f;

        private Vector3 _targetDirection;
        private Vector3 _mouseEdgeTargetDirection;
        private Vector3 _horizontalVelocity;
        private Vector3 _lastPosition;
        private Vector3 _startDrag;
        private float _rotationInput;
        private float _zoomInput;
        private float _zoomHeight;

        private PlayerActions.MapViewActions MapViewActions => inputData.MapViewActions;

        private void InitializeDefaultValues()
        {
            _lastPosition = transform.position;
            _zoomHeight = cameraTransform.localPosition.y;
            cameraTransform.LookAt(transform);
        }

        private void UpdateVelocity()
        {
            _horizontalVelocity = (transform.position - _lastPosition) / Time.deltaTime;
            _horizontalVelocity.y = 0;
            _lastPosition = transform.position;
        }

        public void UpdatePosition(Vector3 targetPosition)
        {
            
            Vector3 zoomTarget = cameraTransform.localPosition;
            zoomTarget.y = focusHeight;
            zoomTarget -= zoomSpeed * (focusHeight - cameraTransform.localPosition.y) * distanceChangeOnZoom *
                          Vector3.forward; //this line move camera on z-axis as much as it moved in y-axis
            Tween.LocalPosition(cameraTransform, zoomTarget, .5f)
                .OnUpdate(cameraTransform, (target, tween) => { cameraTransform.LookAt(transform); });
            Tween.Position(transform, targetPosition, .5f).OnComplete(OnComplete);
            Tween.Rotation(transform, Quaternion.identity, .5f);

            void OnComplete()
            {
                _lastPosition = transform.position;
                _zoomHeight = focusHeight;
                cameraTransform.LookAt(transform);
            }
        }

        private void UpdatePosition()
        {
            var targetDirection = _targetDirection + _mouseEdgeTargetDirection;
            targetDirection.Normalize();
            if (targetDirection.sqrMagnitude > 0.1f)
            {
                currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, acceleration * Time.deltaTime);
                var targetPosition = (cameraTransform.right * targetDirection.x) + cameraTransform.forward * targetDirection.z;
                targetPosition.y = 0f;
                transform.position += targetPosition * (currentSpeed * Time.deltaTime);
                return;
            }

            _horizontalVelocity = Vector3.Lerp(_horizontalVelocity, Vector3.zero, damping * Time.deltaTime);
            transform.position += _horizontalVelocity * Time.deltaTime;
            currentSpeed = 0f; //TODO: ??
        }

        private void UpdateRotation()
        {
            //TODO: Add smoothness to rotation
            transform.rotation = Quaternion.Euler(0f, _rotationInput * maxRotationSpeed + transform.eulerAngles.y, 0f);
        }

        private void Zoom()
        {
            _zoomHeight = Mathf.Clamp(cameraTransform.localPosition.y + _zoomInput * stepSize, minHeight, maxHeight);
            Vector3 zoomTarget = cameraTransform.localPosition;
            zoomTarget.y = _zoomHeight;
            zoomTarget -= zoomSpeed * (_zoomHeight - cameraTransform.localPosition.y) * distanceChangeOnZoom *
                          Vector3.forward; //this line move camera on z-axis as much as it moved in y-axis
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, zoomTarget, zoomDampening * Time.deltaTime);
            cameraTransform.LookAt(transform);
        }

        private void CheckMouseAtScreenEdge()
        {
            if (!useScreenEdge) return;
            var mousePosition = MapViewActions.MosePosition.ReadValue<Vector2>();
            var moveDirection = Vector3.zero;
            CheckHorizontalMovement();
            CheckVerticalMovement();

            _mouseEdgeTargetDirection = moveDirection;

            void CheckHorizontalMovement()
            {
                if (mousePosition.x < edgeTolerance * Screen.width)
                {
                    moveDirection.x -= 1f;
                }
                else if (mousePosition.x > (1f - edgeTolerance) * Screen.width)
                {
                    moveDirection.x += 1f;
                }
            }

            void CheckVerticalMovement()
            {
                if (mousePosition.y < edgeTolerance * Screen.height)
                {
                    moveDirection.z -= 1f;
                }
                else if (mousePosition.y > (1f - edgeTolerance) * Screen.height)
                {
                    moveDirection.z += 1f;
                }
            }
        }

        //TODO: Add drag camera effect with mouse

        #region Input System

        private void OnMove(InputAction.CallbackContext input)
        {
            var inputValue = input.ReadValue<Vector2>();
            var newTargetDirection = new Vector3(inputValue.x, 0f, inputValue.y);
            _targetDirection = newTargetDirection;
        }

        private void OnRotate(InputAction.CallbackContext input)
        {
            _rotationInput = input.ReadValue<float>();
        }

        private void OnMouseRotate(InputAction.CallbackContext input)
        {
            var inputReadingCoroutine = StartCoroutine(ReadMouseRotationInput());
            MapViewActions.MouseRotation.canceled += StopMouseRotationInputReading;

            IEnumerator ReadMouseRotationInput()
            {
                while (true)
                {
                    _rotationInput = MapViewActions.MouseDelta.ReadValue<Vector2>().x;
                    yield return null;
                }
            }

            void StopMouseRotationInputReading(InputAction.CallbackContext context)
            {
                MapViewActions.MouseRotation.canceled -= StopMouseRotationInputReading;
                _rotationInput = 0f;
                StopCoroutine(inputReadingCoroutine);
            }
        }

        private void OnZoom(InputAction.CallbackContext input)
        {
            _zoomInput = input.ReadValue<float>();
        }

        #endregion

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            InitializeDefaultValues();
            MapViewActions.Move.performed += OnMove;
            MapViewActions.Rotate.performed += OnRotate;
            MapViewActions.Zoom.performed += OnZoom;
            MapViewActions.MouseRotation.performed += OnMouseRotate;
        }


        private void OnDisable()
        {
            MapViewActions.Move.performed -= OnMove;
            MapViewActions.Rotate.performed -= OnRotate;
            MapViewActions.Zoom.performed -= OnZoom;
            MapViewActions.MouseRotation.performed -= OnMouseRotate;
        }

        private void Update()
        {
            if (!inputData.IsOnMapViewState) return;

            CheckMouseAtScreenEdge();
            UpdateVelocity();
            UpdatePosition();
            UpdateRotation();
            Zoom();
        }

        #endregion
    }
}