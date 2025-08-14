using AdventurerVillage.LevelSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.GateSystem.UI
{
    public class GateIconView : MonoBehaviour
    {
        [SerializeField] private GradeTable gradeTable;
        [SerializeField] private Image iconImage;
        [SerializeField] private Image iconFrameImage;
        [SerializeField] private TMP_Text gradeText;

        public void Initialize(Sprite gateIcon, Grade grade)
        {
            iconImage.sprite = gateIcon;
            var gradeColor = gradeTable.GetColor(grade);
            gradeText.color = gradeColor;
            iconFrameImage.color = gradeColor;
            gradeText.text = $"{grade}";
        }
    }
}