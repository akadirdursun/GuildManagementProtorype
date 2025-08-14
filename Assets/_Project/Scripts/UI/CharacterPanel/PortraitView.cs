using AdventurerVillage.CharacterSystem;
using AdventurerVillage.CharacterSystem.UI;
using AdventurerVillage.UI.ScreenControlSystems;
using UnityEngine;
using UnityEngine.UI;
using CharacterInfo = AdventurerVillage.CharacterSystem.CharacterInfo;

namespace AdventurerVillage.UI.CharacterPanel
{
    public class PortraitView : MonoBehaviour
    {
        [SerializeField] private SelectedCharacterData selectedCharacterData;
        [SerializeField] private RawImage portraitImage;
        [SerializeField] private Button portraitButton;
        
        private CharacterInfo _characterInfo;

        #region MonoBehaviour

        private void OnDisable()
        {
            portraitButton.onClick.RemoveAllListeners();
        }

        #endregion
        
        public void Initialize(CharacterInfo characterInfo)
        {
            _characterInfo = characterInfo;
            SetPortrait();
            portraitButton.onClick.AddListener(OnPortraitButtonClicked);
        }

        private void OnPortraitButtonClicked()
        {
            selectedCharacterData.SelectCharacter(_characterInfo);
            UIScreenController.Instance.ShowScreen(typeof(CharacterDetailScreen));
        }

        private void SetPortrait()
        {
            portraitImage.texture = _characterInfo.PortraitTexture;
        }
    }
}