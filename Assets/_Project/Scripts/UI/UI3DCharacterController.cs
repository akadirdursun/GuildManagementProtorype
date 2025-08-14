using System.Collections;
using ADK.Common;
using AdventurerVillage.CharacterSystem;
using AdventurerVillage.CharacterSystem.CharacterCustomization;
using AdventurerVillage.Utilities;
using UnityEngine;
using CharacterInfo = AdventurerVillage.CharacterSystem.CharacterInfo;

namespace AdventurerVillage.UI
{
    public class UI3DCharacterController : Singleton<UI3DCharacterController>
    {
        [SerializeField] private SelectedCharacterData selectedCharacterData;
        [SerializeField] private RenderTexture portraitRenderTexture;
        [SerializeField/*, SceneObjectsOnly*/] private GameObject videoCamera;
        [SerializeField/*, SceneObjectsOnly*/] private GameObject portraitCamera;
        [SerializeField/*, SceneObjectsOnly*/] private CharacterCustomizationController characterCustomizationController;

        private Texture2D ClipRenderTexture()
        {
            var newTexture = new RenderTexture(portraitRenderTexture.width, portraitRenderTexture.height,
                portraitRenderTexture.depth);
            Graphics.Blit(portraitRenderTexture, newTexture);
            newTexture.name = "Test1";
            return newTexture.ConvertToTexture2D();
        }

        public void EnableVideoCamera()
        {
            characterCustomizationController.Initialize(selectedCharacterData.SelectedCharacterInfo.CharacterModelInfo);
            characterCustomizationController.gameObject.SetActive(true);
            videoCamera.SetActive(true);
        }

        public void DisableVideoCamera()
        {
            characterCustomizationController.gameObject.SetActive(false);
            videoCamera.SetActive(false);
        }

        public void ClipTexture(CharacterInfo characterInfo)
        {
            var isCharacterActive = characterCustomizationController.gameObject.activeInHierarchy;
            if (!isCharacterActive)
            {
                characterCustomizationController.Initialize(characterInfo.CharacterModelInfo);
                characterCustomizationController.gameObject.SetActive(true);
            }

            portraitCamera.SetActive(true);
            StartCoroutine(Clip());

            IEnumerator Clip()
            {
                yield return new WaitForEndOfFrame();
                var texture = ClipRenderTexture();
                characterInfo.SetPortrait(texture);
                characterCustomizationController.gameObject.SetActive(isCharacterActive);
                portraitCamera.SetActive(false);
            }
        }
    }
}