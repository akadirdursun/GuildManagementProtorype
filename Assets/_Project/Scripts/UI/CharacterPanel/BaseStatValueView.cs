using AdventurerVillage.StatSystem;
using TMPro;
using UnityEngine;

namespace AdventurerVillage.UI.CharacterPanel
{
    public class BaseStatValueView : MonoBehaviour
    {
        [SerializeField] private TMP_Text valueText;
        [SerializeField] private char prefix;
        [SerializeField] private char suffix;

        private Stat _stat;

        public void Initialize(Stat stat)
        {
            UnregisterEvent();
            _stat = stat;
            UpdateValueText();
            RegisterEvent();
        }

        public void Initialize(float value)
        {
            UnregisterEvent();
            _stat = null;
            UpdateValueText($"{value}");
        }

        private void UpdateValueText()
        {
            valueText.text = $"{prefix}{_stat.Value}{suffix}";
        }

        private void UpdateValueText(string value)
        {
            valueText.text = $"{prefix}{value}{suffix}";
        }

        private void RegisterEvent()
        {
            if (_stat == null) return;
            _stat.OnStatValueChanged += UpdateValueText;
        }

        private void UnregisterEvent()
        {
            if (_stat == null) return;
            _stat.OnStatValueChanged -= UpdateValueText;
        }
    }
}