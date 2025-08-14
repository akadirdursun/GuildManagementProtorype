using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.MainMenu
{
    [RequireComponent(typeof(Button))]
    public class ExitButton : MonoBehaviour
    {
        [SerializeField] private Button exitButton;

        private void Awake()
        {
            if (exitButton == null)
            {
                Debug.LogError($"Button is null on {name}");
                exitButton = GetComponent<Button>();
            }
        }

        private void OnEnable()
        {
            exitButton.onClick.AddListener(OnExiButtonClicked);
        }

        private void OnDisable()
        {
            exitButton.onClick.RemoveListener(OnExiButtonClicked);
        }

        private void OnExiButtonClicked()
        {
            Application.Quit();
        }
    }
}