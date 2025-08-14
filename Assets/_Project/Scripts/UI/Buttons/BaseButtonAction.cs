using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public abstract class BaseButtonAction : MonoBehaviour
    {
        private Button _button;

        protected abstract void OnButtonClicked();

        #region MonoBehaviour Methods

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        #endregion
    }
}