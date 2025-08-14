using AdventurerVillage.LevelSystem;
using TMPro;
using UnityEngine;

namespace AdventurerVillage.UI.CharacterPanel
{
    public class CharacterStatView : MonoBehaviour
    {
        [SerializeField] private TMP_Text valueText;

        public void SetStat(float statValue, Grade grade)
        {
            valueText.text = $"{statValue:N2} ({grade})";
        }
    }
}