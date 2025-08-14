using UnityEngine;

namespace AdventurerVillage.CharacterSystem.CharacterCustomization
{
    [CreateAssetMenu(fileName = "CharacterRandomizer",
        menuName = "Adventurer Village/Character Customization/Character Randomizer")]
    public class CharacterModelRandomizer : ScriptableObject
    {
        [Header("Common Mesh")]
        [SerializeField] private Mesh[] hairMeshes;
        [Header("Male Mesh")]
        [SerializeField] private Mesh[] maleHeadMeshes;
        [SerializeField] private Mesh[] maleEyebrowMeshes;
        [SerializeField] private Mesh[] maleFacialHearMeshes;
        [Header("Female Mesh")]
        [SerializeField] private Mesh[] femaleHeadMeshes;
        [SerializeField] private Mesh[] femaleEyebrowMeshes;
        [SerializeField, Space] private Color[] skinColors;
        [SerializeField] private Color[] hairColors;

        public CharacterModelInfo RandomizeCharacter()
        {
            var gender = Random.Range(0, 2) % 2 == 0 ? Genders.Male : Genders.Female;
            var skinColor = skinColors[Random.Range(0, skinColors.Length)];
            var hairColor = hairColors[Random.Range(0, hairColors.Length)];
            var facialHairMeshIndex = gender != Genders.Female && Random.Range(0, 2) % 2 == 0
                ? Random.Range(0, maleFacialHearMeshes.Length)
                : -1;
            var hairMeshIndex = Random.Range(0, hairMeshes.Length);
            var headMeshIndex = GetRandomHeadMeshIndex(gender);
            var eyebrowsMeshIndex = GetRandomEyebrowMeshIndex(gender);
            return new CharacterModelInfo()
            {
                gender = gender,
                skinColor = skinColor,
                hairColor = hairColor,
                hairMeshIndex = hairMeshIndex,
                headMeshIndex = headMeshIndex,
                eyebrowsMeshIndex = eyebrowsMeshIndex,
                facialHairMeshIndex = facialHairMeshIndex
            };
        }

        public CharacterMeshInfo GetCharacterMeshes(CharacterModelInfo characterModelInfo)
        {
            var facialHairMesh = characterModelInfo.gender != Genders.Female && Random.Range(0, 2) % 2 == 0 &&
                                 characterModelInfo.facialHairMeshIndex >= 0
                ? maleFacialHearMeshes[characterModelInfo.facialHairMeshIndex]
                : null;
            var hairMesh = hairMeshes[characterModelInfo.hairMeshIndex];
            var headMesh = GetHeadMesh(characterModelInfo.gender, characterModelInfo.headMeshIndex);
            var eyebrowsMesh = GetEyebrowMesh(characterModelInfo.gender, characterModelInfo.eyebrowsMeshIndex);
            return new CharacterMeshInfo()
            {
                HairMesh = hairMesh,
                HeadMesh = headMesh,
                EyebrowsMesh = eyebrowsMesh,
                FacialHairMesh = facialHairMesh
            };
        }

        private int GetRandomHeadMeshIndex(Genders gender)
        {
            var meshArray = gender == Genders.Female ? femaleHeadMeshes : maleHeadMeshes;
            return Random.Range(0, meshArray.Length);
        }

        private int GetRandomEyebrowMeshIndex(Genders gender)
        {
            var meshArray = gender == Genders.Female ? femaleEyebrowMeshes : maleEyebrowMeshes;
            return Random.Range(0, meshArray.Length);
        }

        private Mesh GetHeadMesh(Genders gender, int index)
        {
            var meshArray = gender == Genders.Female ? femaleHeadMeshes : maleHeadMeshes;
            return meshArray[index];
        }

        private Mesh GetEyebrowMesh(Genders gender, int index)
        {
            var meshArray = gender == Genders.Female ? femaleEyebrowMeshes : maleEyebrowMeshes;
            return meshArray[index];
        }
    }
}