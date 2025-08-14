using System;
using AdventurerVillage.ClassSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.CharacterSystem.UI
{
    public class CharacterClassInfoPanel : MonoBehaviour
    {
        [SerializeField] private ClassDatabase classDatabase;
        [SerializeField] private SelectedCharacterData selectedCharacterData;
        [SerializeField] private Image classIcon;
        [SerializeField] private TMP_Text classNameText;
        [SerializeField] private TMP_Text classInfoText;
        [SerializeField] private Button changeClassButton;

        private CharacterInfo SelectedCharacter => selectedCharacterData.SelectedCharacterInfo;
        private Action _onChangeClassButtonClicked;

        public void Initialize(Action onChangeClassButtonClicked)
        {
            _onChangeClassButtonClicked = onChangeClassButtonClicked;
            var classData = classDatabase.GetClassData(SelectedCharacter.ClassId);
            classIcon.sprite = classData.ClassIcon;
            classNameText.text = classData.ClassName;
        }

        private void OnChangeClassButtonClicked()
        {
            _onChangeClassButtonClicked.Invoke();
        }

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            changeClassButton.onClick.AddListener(OnChangeClassButtonClicked);
        }

        private void OnDisable()
        {
            changeClassButton.onClick.RemoveListener(OnChangeClassButtonClicked);
        }

        #endregion
    }
}