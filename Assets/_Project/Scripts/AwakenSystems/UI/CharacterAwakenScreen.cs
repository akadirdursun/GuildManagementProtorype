using System.Collections;
using AdventurerVillage.CharacterSystem;
using AdventurerVillage.CharacterSystem.CharacterCustomization;
using AdventurerVillage.CharacterSystem.UI;
using AdventurerVillage.UI.ScreenControlSystems;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.AwakenSystems.UI
{
    public class CharacterAwakenScreen : UIScreen
    {
        [SerializeField] private SelectedCharacterData selectedCharacter;
        [SerializeField] private CharacterAwakenController characterAwakenController;
        [SerializeField] private CharacterModelRandomizer characterModelRandomizer;
        [SerializeField] private CharacterAwakenGradeController characterAwakenGradeController;
        [SerializeField] private Button awakenButton;

        private CharacterModelInfo _randomCharacterModel;

        private void OnAwakenButtonClicked()
        {
            characterAwakenGradeController.GetAwakenGrade(out var grade, out var maxAttributeGrade);
            var characterInfo =
                characterAwakenController.AwakenCharacter(grade, maxAttributeGrade, _randomCharacterModel);
            selectedCharacter.SelectCharacter(characterInfo);
            StartCoroutine(ShowCharacterDetailScreen());
            GenerateRandomCharacterModel();
        }

        private IEnumerator ShowCharacterDetailScreen()
        {
            yield return new WaitForEndOfFrame();
            UIScreenController.Instance.ShowScreen(typeof(CharacterDetailScreen));
        }

        private void GenerateRandomCharacterModel()
        {
            _randomCharacterModel = characterModelRandomizer.RandomizeCharacter();
            AwakenCenterViewController.Instance.ChangeCharacterModel(_randomCharacterModel);
        }

        #region UIScreen Methods

        protected override void RegisterToEvents()
        {
            base.RegisterToEvents();
            awakenButton.onClick.AddListener(OnAwakenButtonClicked);
        }

        protected override void UnregisterFromEvents()
        {
            base.UnregisterFromEvents();
            awakenButton.onClick.RemoveListener(OnAwakenButtonClicked);
        }

        protected override void OnBeforePopupShow()
        {
            base.OnBeforePopupShow();
            AwakenCenterViewController.Instance.Enable();
        }

        protected override void OnAfterPopupClose()
        {
            base.OnAfterPopupClose();
            AwakenCenterViewController.Instance.Disable();
        }

        #endregion

        #region MonoBehaviour Methods

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => AwakenCenterViewController.IsInstanceExist);
            GenerateRandomCharacterModel();
        }

        #endregion
    }
}