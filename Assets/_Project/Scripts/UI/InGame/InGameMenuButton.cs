using AdventurerVillage.UI.ScreenControlSystems;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.UI.InGame
{
    public class InGameMenuButton: MonoBehaviour
    {
        [SerializeField] private Button menuButton;

        private void OnMenuButtonClicked()
        {
            UIScreenController.Instance.ShowScreen(typeof(InGameMenu));
        }
        
        #region MonoBehaviour Methods

        private void OnEnable()
        {
            menuButton.onClick.AddListener(OnMenuButtonClicked);
        }

        private void OnDisable()
        {
            menuButton.onClick.RemoveListener(OnMenuButtonClicked);
        }

        #endregion
    }
}