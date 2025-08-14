using System;
using AdventurerVillage.UI.ScreenControlSystems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.UI.Test
{
    public class ShowScreenButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text buttonText;
        private UIScreen _uiScreen;

        #region MonoBehaviour Methods

        public void Initialize(UIScreen uiScreen)
        {
            _uiScreen = uiScreen;
            buttonText.text = $"{_uiScreen.name}";
        }

        private void OnEnable()
        {
            button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnButtonClicked);
        }

        #endregion

        private void OnButtonClicked()
        {
            UIScreenController.Instance.ShowScreen(_uiScreen.GetType());
        }
    }
}