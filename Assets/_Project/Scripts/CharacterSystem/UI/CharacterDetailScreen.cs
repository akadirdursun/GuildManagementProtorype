using AdventurerVillage.UI;
using AdventurerVillage.UI.CharacterPanel;
using AdventurerVillage.UI.ScreenControlSystems;
using UnityEngine;

namespace AdventurerVillage.CharacterSystem.UI
{
    public class CharacterDetailScreen : UIScreen
    {
        [SerializeField] private SelectedCharacterData selectedCharacterData;
        [SerializeField] private CharacterScreenInfoAreaArea characterScreenInfoAreaArea;
        [SerializeField] private StatAreaController statAreaController;
        [SerializeField] private TabGroupController tabGroupController;
        [SerializeField] private PanelMoveAnimation[] animationControllers;
        
        private void ShowCharacterPanels()
        {
            var characterInfo = selectedCharacterData.SelectedCharacterInfo;
            characterScreenInfoAreaArea.Initialize(characterInfo);
            statAreaController.Initialize(characterInfo.Stats);
            var tabGroupIndex = characterInfo.ClassAssigned ? 1 : 0;
            tabGroupController.ActivateTab(tabGroupIndex);
            UI3DCharacterController.Instance.EnableVideoCamera();
            foreach (var controller in animationControllers)
            {
                controller.PlayMoveAnimation();
            }
        }

        private void HideCharacterPanels()
        {
            foreach (var controller in animationControllers)
            {
                controller.HidePanel();
            }
        }
        
        #region UIScreen Methods

        protected override void OnBeforePopupShow()
        {
            base.OnBeforePopupShow();
            HideCharacterPanels();
        }

        protected override void OnAfterPopupShow()
        {
            base.OnAfterPopupShow();
            ShowCharacterPanels();
        }

        protected override void OnAfterPopupClose()
        {
            base.OnAfterPopupClose();
            UI3DCharacterController.Instance.DisableVideoCamera();
        }

        #endregion
    }
}