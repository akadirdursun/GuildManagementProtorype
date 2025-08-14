using AdventurerVillage.EffectSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.StatSystem.UI
{
    public class StatView : MonoBehaviour
    {
        [SerializeField] private Image backgroundImage;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text valueText;
        [SerializeField] private Color defaultTextColor;
        [SerializeField] private Color positiveTextColor;
        [SerializeField] private Color negativeTextColor;

        private StatTypes _statType;
        private Stat _stat;
        private string _suffix;

        public StatTypes StatType => _statType;

        public void Initialize(Stat stat, StatTypes statType, string title, string suffix)
        {
            UnregisterFromEvents();
            _stat = stat;
            _statType = statType;
            RegisterToEvents();
            _suffix = suffix;
            titleText.text = title;
            UpdateValueText();
        }

        public void Initialize(Stat stat, StatTypes statType, string title, string suffix, Color backgroundColor)
        {
            UnregisterFromEvents();
            _stat = stat;
            _statType = statType;
            RegisterToEvents();
            _suffix = suffix;
            titleText.text = title;
            UpdateValueText();
            backgroundImage.color = backgroundColor;
        }

        public void UpdateValueText()
        {
            valueText.color = defaultTextColor;
            valueText.text = $"{_stat.Value:N1}{_suffix}";
        }

        public void UpdateValueText(EffectInfo effectInfo)
        {
            var difference = _stat.GetEffectChange(effectInfo);
            var newValue = _stat.Value + difference;
            var color = difference > 0 ? positiveTextColor : difference < 0 ? negativeTextColor : defaultTextColor;
            valueText.color = color;
            valueText.text = $"{newValue:N1}{_suffix}";
        }

        private void RegisterToEvents()
        {
            if (_stat == null) return;
            _stat.OnStatValueChanged += UpdateValueText;
        }

        private void UnregisterFromEvents()
        {
            if (_stat == null) return;
            _stat.OnStatValueChanged -= UpdateValueText;
        }

        #region MonoBerhaviour Methods

        private void OnDisable()
        {
            UnregisterFromEvents();
            _stat = null;
        }

        #endregion
    }
}