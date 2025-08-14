using System.Collections;
using AdventurerVillage.InputControl.Enums;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AdventurerVillage.InputControl
{
    [CreateAssetMenu(fileName = "InputData", menuName = "Adventurer Village/Input Control/Input Data")]
    public class InputData : ScriptableObject
    {
        private InputStates _currentState;
        public PlayerActions.MapViewActions MapViewActions { get; private set; }
        public PlayerActions.UIActions UIScreenActions { get; private set; }
        public bool IsOnMapViewState => _currentState == InputStates.MapView;
        public bool UIScreenState => _currentState == InputStates.UIScreen;

        public bool IsPointerOverGameObject { get; private set; }

        public void Initialize()
        {
            var playerActions = new PlayerActions();
            MapViewActions = playerActions.MapView;
            UIScreenActions = playerActions.UI;
            EnableMapViewState();
        }

        public void DisableAll()
        {
            DisableMapViewState();
            DisableUIScreenState();
        }

        public void ChangeState(InputStates newState)
        {
            if (_currentState == newState)
            {
                Debug.LogWarning($"Current input state already on {newState}");
                return;
            }

            switch (newState)
            {
                case InputStates.MapView:
                    DisableUIScreenState();
                    EnableMapViewState();
                    break;
                case InputStates.UIScreen:
                    DisableMapViewState();
                    EnableUIScreenState();
                    break;
            }
        }

        public IEnumerator CheckIfPointerOverGameObject()
        {
            while (true)
            {
                yield return new WaitForFixedUpdate();
                IsPointerOverGameObject = EventSystem.current.IsPointerOverGameObject();
            }
        }

        private void EnableMapViewState()
        {
            MapViewActions.Enable();
            _currentState = InputStates.MapView;
        }

        private void DisableMapViewState()
        {
            MapViewActions.Disable();
        }

        private void EnableUIScreenState()
        {
            UIScreenActions.Enable();
            _currentState = InputStates.UIScreen;
        }

        private void DisableUIScreenState()
        {
            UIScreenActions.Disable();
        }
    }
}