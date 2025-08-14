using System;
using AdventurerVillage.ClassSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.CharacterSystem.UI
{
    public class ClassButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private Image image;
        [SerializeField] private Button button;

        private BaseClass _classData;
        private Action<BaseClass> _onClickAction;

        public void Initialize(BaseClass classData, Action<BaseClass> onClickAction)
        {
            _classData = classData;
            _onClickAction = onClickAction;
            nameText.text = _classData.ClassName;
            image.sprite = _classData.ClassIcon;
        }

        private void OnButtonClicked()
        {
            _onClickAction.Invoke(_classData);
        }

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnButtonClicked);
        }

        #endregion
    }
}