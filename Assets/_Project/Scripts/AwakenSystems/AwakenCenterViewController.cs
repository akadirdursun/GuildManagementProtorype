using ADK.Common;
using AdventurerVillage.CharacterSystem;
using AdventurerVillage.CharacterSystem.CharacterCustomization;
using UnityEngine;

namespace AdventurerVillage.AwakenSystems
{
    public class AwakenCenterViewController : Singleton<AwakenCenterViewController>
    {
        [SerializeField] private SelectedCharacterData selectedCharacterData;
        [SerializeField] private CharacterCustomizationController characterCustomizationController;
        [SerializeField] private GameObject cameraObject;

        public void Enable()
        {
            cameraObject.SetActive(true);
        }

        public void Disable()
        {
            cameraObject.SetActive(false);
        }

        public void ChangeCharacterModel(CharacterModelInfo characterModelInfo)
        {
            characterCustomizationController.Initialize(characterModelInfo);
        }
    }
}