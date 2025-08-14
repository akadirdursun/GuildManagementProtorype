using AdventurerVillage.ClassSystem.UI;
using AdventurerVillage.StatSystem.UI;
using AdventurerVillage.UI.CharacterPanel;
using TMPro;
using UnityEngine;

namespace AdventurerVillage.CharacterSystem.UI
{
    public class CharacterCard : MonoBehaviour
    {
        [SerializeField] private ClassIcon classIcon;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private PortraitView portraitView;
        [Header("Attributes")]
        [SerializeField] private CircleCharacterAttributeView strengthView;
        [SerializeField] private CircleCharacterAttributeView constitutionView;
        [SerializeField] private CircleCharacterAttributeView agilityView;
        [SerializeField] private CircleCharacterAttributeView intelligenceView;
        [SerializeField] private CircleCharacterAttributeView magicView;
        [Header("Vitals")]
        [SerializeField] private VitalView healthView;
        [SerializeField] private VitalView staminaView;
        [SerializeField] private VitalView manaView;

        protected CharacterInfo _characterInfo;

        public void Initialize(CharacterInfo characterInfo)
        {
            _characterInfo = characterInfo;
            var characterStats = _characterInfo.Stats;
            //TODO: Set class icon
            nameText.text = characterInfo.Name;
            portraitView.Initialize(characterInfo);
            //Attributes
            strengthView.Initialize(characterStats.Strength);
            constitutionView.Initialize(characterStats.Constitution);
            agilityView.Initialize(characterStats.Agility);
            intelligenceView.Initialize(characterStats.Intelligence);
            magicView.Initialize(characterStats.Magic);
            //Vitals
            healthView.Initialize(characterStats.Health);
            staminaView.Initialize(characterStats.Stamina);
            manaView.Initialize(characterStats.Mana);
        }
    }
}