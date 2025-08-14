using TMPro;
using UnityEngine;
using CharacterInfo = AdventurerVillage.CharacterSystem.CharacterInfo;

namespace AdventurerVillage.UI.CharacterPanel
{
    public class CharacterScreenInfoAreaArea : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameText;
        [SerializeField/*, TabGroup("Attribute Views")*/] private CharacterStatView strengthAttributeView;
        [SerializeField/*, TabGroup("Attribute Views")*/] private CharacterStatView constitutionAttributeView;
        [SerializeField/*, TabGroup("Attribute Views")*/] private CharacterStatView agilityAttributeView;
        [SerializeField/*, TabGroup("Attribute Views")*/] private CharacterStatView intelligenceAttributeView;
        [SerializeField/*, TabGroup("Attribute Views")*/] private CharacterStatView magicAttributeView;

        public void Initialize(CharacterInfo characterInfo)
        {
            nameText.text = characterInfo.Name;
            var characterStats = characterInfo.Stats;
            strengthAttributeView.SetStat(characterStats.Strength.Level, characterStats.Strength.Grade);
            constitutionAttributeView.SetStat(characterStats.Constitution.Level, characterStats.Constitution.Grade);
            agilityAttributeView.SetStat(characterStats.Agility.Level, characterStats.Agility.Grade);
            intelligenceAttributeView.SetStat(characterStats.Intelligence.Level, characterStats.Intelligence.Grade);
            magicAttributeView.SetStat(characterStats.Magic.Level, characterStats.Magic.Grade);
        }
    }
}