using AdventurerVillage.LoadingSystem;
using AdventurerVillage.SaveSystem;
using AdventurerVillage.SceneLoadSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.MainMenu
{
    public class SaveSlotButton : MonoBehaviour
    {
        [SerializeField] private SaveLoadManager saveLoadManager;
        [SerializeField] private GameObject slotNameObject;
        [SerializeField] private Button slotButton;
        [SerializeField] private Button deleteButton;
        [SerializeField] private Button confirmNameButton;
        [SerializeField] private TMP_InputField slotNameInputField;
        [SerializeField] private TMP_Text slotNameText;
        [SerializeField] private TMP_Text timeText;

        private int _slotIndex;

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            slotButton.onClick.AddListener(OnSlotSelected);
            deleteButton.onClick.AddListener(DeleteSaveSlot);
            confirmNameButton.onClick.AddListener(OnConfirmNameButtonClicked);
        }

        private void OnDisable()
        {
            slotButton.onClick.RemoveListener(OnSlotSelected);
            deleteButton.onClick.RemoveListener(DeleteSaveSlot);
            confirmNameButton.onClick.RemoveListener(OnConfirmNameButtonClicked);
        }

        #endregion

        public void Initialize(int slotIndex)
        {
            _slotIndex = slotIndex;
            SetView();
        }

        private void SetView()
        {
            var slotExist = saveLoadManager.SaveFileExists(_slotIndex);
            deleteButton.gameObject.SetActive(slotExist);
            if (!slotExist)
            {
                slotNameText.text = $"Save Slot {_slotIndex}";
                timeText.gameObject.SetActive(false);
                return;
            }

            var saveSlotInfo = saveLoadManager.GetSaveSlotInfo(_slotIndex);
            slotNameText.text = saveSlotInfo.slotName;
            timeText.text = saveSlotInfo.lastSaveTime;
            timeText.gameObject.SetActive(true);
        }

        private void OnSlotSelected()
        {
            if (!saveLoadManager.SaveFileExists(_slotIndex))
            {
                slotNameObject.SetActive(true);
                slotNameInputField.Select();
                return;
            }

            saveLoadManager.SelectSavePath(_slotIndex);
            saveLoadManager.LoadGame();
            SceneLoader.Instance.StartGameLoading(GameLoadType.LoadPreviousGame);
        }

        private void OnConfirmNameButtonClicked()
        {
            var selectedName = slotNameInputField.text;
            if (string.IsNullOrEmpty(selectedName))
            {
                selectedName = $"Game {_slotIndex + 1}";
                slotNameInputField.text = selectedName;
            }
            saveLoadManager.ResetAllScriptableObjects();
            saveLoadManager.CreateSaveSlot(selectedName, _slotIndex);
            saveLoadManager.SelectSavePath(_slotIndex);
            SceneLoader.Instance.StartGameLoading(GameLoadType.LoadNewGame);
        }

        private void DeleteSaveSlot()
        {
            saveLoadManager.DeleteSavePath(_slotIndex);
            SetView();
        }
    }
}