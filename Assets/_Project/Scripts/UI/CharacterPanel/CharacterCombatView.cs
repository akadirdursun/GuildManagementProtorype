using AdventurerVillage.LevelSystem;
using AdventurerVillage.StatSystem.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CharacterInfo = AdventurerVillage.CharacterSystem.CharacterInfo;

namespace AdventurerVillage.UI.CharacterPanel
{
    public class CharacterCombatView : MonoBehaviour
    {
        [SerializeField] private GradeTable gradeTable;
        [SerializeField] private TMP_Text characterNameText;
        [SerializeField] private TMP_Text characterGradeText;
        [SerializeField] private RawImage characterPortraitImage;
        [SerializeField] private VitalView healthVitalView;
        [SerializeField] private VitalView staminaVitalView;
        [SerializeField] private VitalView manaVitalView;
        
        public void Initialize(CharacterInfo characterInfo)
        {
            characterNameText.text = characterInfo.Name;
            var characterGrade = gradeTable.GetGradeFromLevel(characterInfo.AverageLevel);
            characterGradeText.text = $"{characterGrade}";
            characterPortraitImage.texture = characterInfo.PortraitTexture;
            
            var characterStats = characterInfo.Stats;
            healthVitalView.Initialize(characterStats.Health); 
            staminaVitalView.Initialize(characterStats.Stamina);
            manaVitalView.Initialize(characterStats.Mana);
        }
    }
}
