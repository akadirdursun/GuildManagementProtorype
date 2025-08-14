using UnityEngine;

namespace AdventurerVillage.CharacterSystem.CharacterCustomization
{
    public partial class CharacterCustomizationController : MonoBehaviour
    {
        [Header("Common Parts")]
        [SerializeField] private Material commonMaterial;
        [SerializeField] private GameObject[] hairParts;
        [Header("Male Parts")]
        [SerializeField] private GameObject maleParts;
        [SerializeField] private GameObject[] maleHeadParts;
        [SerializeField] private GameObject[] maleHeadEquipmentParts;
        [SerializeField] private GameObject[] maleEyebrowsParts;
        [SerializeField] private GameObject[] maleFacialHairParts;
        [SerializeField] private GameObject[] maleTorsoParts;
        [SerializeField] private GameObject[] maleHipsParts;
        [SerializeField] private GameObject[] maleRightHandParts;
        [SerializeField] private GameObject[] maleLeftHandParts;
        [SerializeField] private GameObject[] maleRightLegParts;
        [SerializeField] private GameObject[] maleLeftLegParts;
        [Header("Female Parts")]
        [SerializeField] private GameObject femaleParts;
        [SerializeField] private GameObject[] femaleHeadParts;
        [SerializeField] private GameObject[] femaleHeadEquipmentParts;
        [SerializeField] private GameObject[] femaleEyebrowsParts;
        [SerializeField] private GameObject[] femaleTorsoParts;
        [SerializeField] private GameObject[] femaleHipsParts;
        [SerializeField] private GameObject[] femaleRightHandParts;
        [SerializeField] private GameObject[] femaleLeftHandParts;
        [SerializeField] private GameObject[] femaleRightLegParts;
        [SerializeField] private GameObject[] femaleLeftLegParts;

        private int _activeHairIndex;
        //Male
        private int _maleHeadIndex;
        private int _maleHeadEquipmentIndex;
        private int _maleEyebrowsIndex;
        private int _maleFacialHairIndex;
        private int _maleTorsoIndex;
        private int _maleHipsIndex;
        private int _maleRightHandIndex;
        private int _maleLeftHandIndex;
        private int _maleRightLegIndex;
        private int _maleLeftLegIndex;
        //Female
        private int _femaleHeadIndex;
        private int _femaleHeadEquipmentIndex;
        private int _femaleEyebrowsIndex;
        private int _femaleTorsoIndex;
        private int _femaleHipsIndex;
        private int _femaleRightHandIndex;
        private int _femaleLeftHandIndex;
        private int _femaleRightLegIndex;
        private int _femaleLeftLegIndex;

        public void Initialize(CharacterModelInfo characterModelInfo)
        {
            commonMaterial.SetColor("_Color_Skin", characterModelInfo.skinColor);
            commonMaterial.SetColor("_Color_Hair", characterModelInfo.hairColor);
            _activeHairIndex = characterModelInfo.hairMeshIndex;
            if (characterModelInfo.gender == Genders.Female)
            {
                DeactivateFemaleParts();
                _femaleHeadIndex = characterModelInfo.headMeshIndex;
                //_femaleHeadEquipmentIndex=
                _femaleEyebrowsIndex = characterModelInfo.eyebrowsMeshIndex;
                //_femaleTorsoIndex=
                //_femaleHipsIndex=
                //_femaleRightHandIndex=
                //_femaleLeftHandIndex=
                //_femaleRightLegIndex=
                //_femaleLeftLegIndex=
                ActivateFemaleParts();
                return;
            }

            DeactivateMaleParts();
            _maleHeadIndex = characterModelInfo.headMeshIndex;
            //_maleHeadEquipmentIndex=
            _maleEyebrowsIndex = characterModelInfo.eyebrowsMeshIndex;
            _maleFacialHairIndex = characterModelInfo.facialHairMeshIndex;
            //_maleTorsoIndex=
            //_maleHipsIndex=
            //_maleRightHandIndex=
            //_maleLeftHandIndex=
            //_maleRightLegIndex=
            //_maleLeftLegIndex=
            ActivateMaleParts();
        }

        private void ActivateMaleParts()
        {
            femaleParts.SetActive(false);
            maleParts.SetActive(true);
            hairParts[_activeHairIndex].SetActive(true);
            maleHeadParts[_maleHeadIndex].SetActive(true);
            //maleHeadEquipmentParts[_maleHeadEquipmentIndex].SetActive(true);
            maleEyebrowsParts[_maleEyebrowsIndex].SetActive(true);
            if (_maleFacialHairIndex >= 0)
                maleFacialHairParts[_maleFacialHairIndex].SetActive(true);
            //maleTorsoParts[_maleTorsoIndex].SetActive(true);
            //maleHipsParts[_maleHipsIndex].SetActive(true);
            //maleRightHandParts[_maleRightHandIndex].SetActive(true);
            //maleLeftHandParts[_maleLeftHandIndex].SetActive(true);
            //maleRightLegParts[_maleRightLegIndex].SetActive(true);
            //maleLeftLegParts[_maleLeftLegIndex].SetActive(true);
        }

        private void ActivateFemaleParts()
        {
            maleParts.SetActive(false);
            femaleParts.SetActive(true);
            hairParts[_activeHairIndex].SetActive(true);
            femaleHeadParts[_femaleHeadIndex].SetActive(true);
            //femaleHeadEquipmentParts[_femaleHeadEquipmentIndex].SetActive(true);
            femaleEyebrowsParts[_femaleEyebrowsIndex].SetActive(true);
            //femaleTorsoParts[_femaleTorsoIndex].SetActive(true);
            //femaleHipsParts[_femaleHipsIndex].SetActive(true);
            //femaleRightHandParts[_femaleRightHandIndex].SetActive(true);
            //femaleLeftHandParts[_femaleLeftHandIndex].SetActive(true);
            //femaleRightLegParts[_femaleRightLegIndex].SetActive(true);
            //femaleLeftLegParts[_femaleLeftLegIndex].SetActive(true);
        }

        private void DeactivateMaleParts()
        {
            hairParts[_activeHairIndex].SetActive(false);
            maleHeadParts[_maleHeadIndex].SetActive(false);
            //maleHeadEquipmentParts[_maleHeadEquipmentIndex].SetActive(false);
            maleEyebrowsParts[_maleEyebrowsIndex].SetActive(false);
            if (_maleFacialHairIndex >= 0)
                maleFacialHairParts[_maleFacialHairIndex].SetActive(false);
            //maleTorsoParts[_maleTorsoIndex].SetActive(false);
            //maleHipsParts[_maleHipsIndex].SetActive(false);
            //maleRightHandParts[_maleRightHandIndex].SetActive(false);
            //maleLeftHandParts[_maleLeftHandIndex].SetActive(false);
            //maleRightLegParts[_maleRightLegIndex].SetActive(false);
            //maleLeftLegParts[_maleLeftLegIndex].SetActive(false);
        }

        private void DeactivateFemaleParts()
        {
            hairParts[_activeHairIndex].SetActive(false);
            femaleHeadParts[_femaleHeadIndex].SetActive(false);
            //femaleHeadEquipmentParts[_femaleHeadEquipmentIndex].SetActive(false);
            femaleEyebrowsParts[_femaleEyebrowsIndex].SetActive(false);
            //femaleTorsoParts[_femaleTorsoIndex].SetActive(false);
            //femaleHipsParts[_femaleHipsIndex].SetActive(false);
            //femaleRightHandParts[_femaleRightHandIndex].SetActive(false);
            //femaleLeftHandParts[_femaleLeftHandIndex].SetActive(false);
            //femaleRightLegParts[_femaleRightLegIndex].SetActive(false);
            //femaleLeftLegParts[_femaleLeftLegIndex].SetActive(false);
        }

   
    }
}