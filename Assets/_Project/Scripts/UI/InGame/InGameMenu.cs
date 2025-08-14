using AdventurerVillage.InputControl;
using AdventurerVillage.SaveSystem;
using AdventurerVillage.SceneLoadSystem;
using AdventurerVillage.TimeSystem;
using AdventurerVillage.UI.ScreenControlSystems;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.UI.InGame
{
    public class InGameMenu : UIScreen
    {
        [SerializeField] private InputData inputData;
        [SerializeField] private TimeSettings timeSettings;
        [SerializeField] private SaveLoadManager saveLoadManager;
        [SerializeField] private Button saveButton;
        [SerializeField] private Button returnToMainMenuButton;

        private int _gameSpeed;
        private void OnReturnToMainMenuClicked()
        {
            inputData.DisableAll();
            SceneLoader.Instance.ReturnToMainMenuFromInGame();
        }

        private void OnSaveButtonClicked()
        {
            saveLoadManager.SaveGame();
        }

        #region UIScreen Methods

        protected override void OnBeforePopupShow()
        {
            base.OnBeforePopupShow();
            _gameSpeed = timeSettings.TimeSpeedMultiplier;
            timeSettings.SetTimeSpeedMultiplier(0);
        }

        protected override void OnAfterPopupClose()
        {
            base.OnAfterPopupClose();
            timeSettings.SetTimeSpeedMultiplier(_gameSpeed);
        }

        protected override void RegisterToEvents()
        {
            base.RegisterToEvents();
            returnToMainMenuButton.onClick.AddListener(OnReturnToMainMenuClicked);
            saveButton.onClick.AddListener(OnSaveButtonClicked);
        }

        protected override void UnregisterFromEvents()
        {
            base.UnregisterFromEvents();
            returnToMainMenuButton.onClick.RemoveListener(OnReturnToMainMenuClicked);
            saveButton.onClick.RemoveListener(OnSaveButtonClicked);
        }

        #endregion
        
    }
}