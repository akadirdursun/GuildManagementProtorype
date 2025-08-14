using UnityEngine;

namespace AdventurerVillage.CharacterSystem.UI
{
    public class CharacterDetailClassAreaController : MonoBehaviour
    {
        [SerializeField] private SelectedCharacterData selectedCharacterData;
        [SerializeField] private CharacterClassInfoPanel characterClassInfoPanel;
        [SerializeField] private ClassSelectionPanel characterSelectionPanel;


        public void OnBeforeCharacterDetailScreenOpen()
        {
            selectedCharacterData.SelectedCharacterInfo.OnCharacterClassChanged += SetView;
            SetView();
        }

        public void OnAfterCharacterDetailScreenClosed()
        {
            selectedCharacterData.SelectedCharacterInfo.OnCharacterClassChanged -= SetView;
        }
        
        private void SetView()
        {
            if (selectedCharacterData.SelectedCharacterInfo.ClassAssigned)
            {
                ShowClassInfoPanel();
                return;
            }
            
            ShowClassSelectionPanel();
        }

        private void ShowClassSelectionPanel()
        {
            characterClassInfoPanel.gameObject.SetActive(false);   
            characterSelectionPanel.Initialize();
            characterSelectionPanel.gameObject.SetActive(true);   
        }

        private void ShowClassInfoPanel()
        {
            characterSelectionPanel.gameObject.SetActive(false); 
            characterClassInfoPanel.Initialize(ShowClassSelectionPanel);
            characterClassInfoPanel.gameObject.SetActive(true);   
        }
    }
}