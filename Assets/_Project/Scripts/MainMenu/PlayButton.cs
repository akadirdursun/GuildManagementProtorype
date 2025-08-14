using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.MainMenu
{
    [RequireComponent(typeof(Button))]
    public class PlayButton : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private GameObject mainButtonsPanel;
        [SerializeField] private GameObject saveSlotPanel;

        private void Awake()
        {
            if (playButton == null)
            {
                Debug.LogError($"Button is null on {name}");
                playButton = GetComponent<Button>();
            }
        }

        private void OnEnable()
        {
            playButton.onClick.AddListener(OnPlayButtonClicked);
        }

        private void OnDisable()
        {
            playButton.onClick.RemoveListener(OnPlayButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            mainButtonsPanel.SetActive(false);
            saveSlotPanel.SetActive(true);
        }
    }
}