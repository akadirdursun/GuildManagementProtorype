#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace AdventurerVillage.CharacterSystem.CharacterCustomization
{
    public partial class CharacterCustomizationController
    {

        [ContextMenu("Set Material")]
        private void SetMaterial()
        {
            SetLoop(hairParts);
            SetLoop(maleHeadParts);
            SetLoop(maleHeadEquipmentParts);
            SetLoop(maleEyebrowsParts);
            SetLoop(maleFacialHairParts);
            SetLoop(maleTorsoParts);
            SetLoop(maleHipsParts);
            SetLoop(maleRightHandParts);
            SetLoop(maleLeftHandParts);
            SetLoop(maleRightLegParts);
            SetLoop(maleLeftLegParts);
            SetLoop(femaleHeadParts);
            SetLoop(femaleHeadEquipmentParts);
            SetLoop(femaleEyebrowsParts);
            SetLoop(femaleTorsoParts);
            SetLoop(femaleHipsParts);
            SetLoop(femaleRightHandParts);
            SetLoop(femaleLeftHandParts);
            SetLoop(femaleRightLegParts);
            SetLoop(femaleLeftLegParts);
            EditorUtility.SetDirty(this);
            void SetLoop(GameObject[] array)
            {
                foreach (var gameObject in array)
                {
                    if (!gameObject.TryGetComponent<SkinnedMeshRenderer>(out var meshRenderer)) continue;
                    meshRenderer.sharedMaterial = commonMaterial;
                }
            }
        }
    }
}
#endif
