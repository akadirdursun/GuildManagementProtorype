using AdventurerVillage.UI.ScreenControlSystems;
using UnityEngine;

namespace AdventurerVillage.CharacterSystem.UI
{
    public class CharacterListScreen : UIScreen
    {
        [SerializeField] private CharacterDatabase characterDatabase;
        [SerializeField] private CharacterListScroll characterListScroll;

        private void SetCharacterList()
        {
            var characters = characterDatabase.AllCharacters;
            characterListScroll.Initialize(characters);
        }

        #region UIScreen

        protected override void OnBeforePopupShow()
        {
            base.OnBeforePopupShow();
            SetCharacterList();
        }

        #endregion
    }
}