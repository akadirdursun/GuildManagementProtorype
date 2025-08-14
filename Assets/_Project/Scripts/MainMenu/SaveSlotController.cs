using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.MainMenu
{
    public class SaveSlotController : MonoBehaviour
    {
        [SerializeField] private Button backButton;
        [SerializeField] private GameObject mainButtonsPanel;
        [SerializeField] private GameObject saveSlotPanel;
        [SerializeField, Space] private SaveSlotButton[] saveSlotButtons;

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            backButton.onClick.AddListener(OnBackButtonClicked);
        }

        private void Start()
        {
            for (int i = 0; i < saveSlotButtons.Length; i++)
            {
                saveSlotButtons[i].Initialize(i);
            }
        }

        private void OnDisable()
        {
            backButton.onClick.RemoveListener(OnBackButtonClicked);
        }

        #endregion
        

        private void OnBackButtonClicked()
        {
            mainButtonsPanel.SetActive(true);
            saveSlotPanel.SetActive(false);
        }
    }
}