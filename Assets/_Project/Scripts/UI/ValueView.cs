using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.UI
{
    public class ValueView : MonoBehaviour
    {
        [SerializeField] private Image backgroundImage;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text valueText;

        public void Initialize(string title, string value)
        {
            titleText.text = title;
            valueText.text = value;
        }

        public void Initialize(string title, string value, Color backgroundColor)
        {
            Initialize(title, value);
            backgroundImage.color = backgroundColor;
        }
    }
}