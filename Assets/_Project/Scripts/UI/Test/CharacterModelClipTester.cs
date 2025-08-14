using System.Collections;
using AdventurerVillage.CharacterSystem.CharacterCustomization;
using AdventurerVillage.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.UI.Test
{
    public class CharacterModelClipTester : MonoBehaviour
    {
        [SerializeField] private CharacterModelRandomizer characterModelRandomizer;
        [SerializeField] private RenderTexture portraitRenderTexture;
        [SerializeField/*, SceneObjectsOnly*/] private CharacterCustomizationController characterCustomizationController;
        [SerializeField] private RawImage portraitRawImage;
        
        [ContextMenu("Clip")]
        private void ClipTexture()
        {
            characterCustomizationController.Initialize(characterModelRandomizer.RandomizeCharacter());
            StartCoroutine(Clip());

            IEnumerator Clip()
            {
                yield return new WaitForFixedUpdate();
                var portraitTexture = ClipRenderTexture();
                portraitRawImage.texture = portraitTexture;
            }
           
        }
        
        private Texture2D ClipRenderTexture()
        {
            var newTexture = new RenderTexture(portraitRenderTexture.width, portraitRenderTexture.height,
                portraitRenderTexture.depth);
            Graphics.Blit(portraitRenderTexture, newTexture);
            newTexture.name = "Test1";
            return newTexture.ConvertToTexture2D();
        }
    }
}