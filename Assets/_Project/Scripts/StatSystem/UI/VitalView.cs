using ADK.Common.UI;
using TMPro;
using UnityEngine;

namespace AdventurerVillage.StatSystem.UI
{
    public class VitalView : MonoBehaviour
    {
        [SerializeField] private SlicedFilledImage fill;
        [SerializeField] private TMP_Text valueText;

        private VitalStat _vitalStat;

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            if (_vitalStat == null) return;
            _vitalStat.OnVitalCurrentValueChanged += UpdateView;
            _vitalStat.OnStatValueChanged += UpdateView;
        }

        private void OnDisable()
        {
            if (_vitalStat == null) return;
            _vitalStat.OnVitalCurrentValueChanged -= UpdateView;
            _vitalStat.OnStatValueChanged -= UpdateView;
        }

        #endregion

        public void Initialize(VitalStat vital)
        {
            _vitalStat = vital;
            _vitalStat.OnVitalCurrentValueChanged += UpdateView;
            UpdateView();
        }

        private void UpdateView()   
        {
            fill.fillAmount = _vitalStat.CurrentValue / _vitalStat.Value;
            if(valueText==null)return;
            valueText.text = $"{_vitalStat.CurrentValue:N0} / {_vitalStat.Value:N0}";
        }
    }
}