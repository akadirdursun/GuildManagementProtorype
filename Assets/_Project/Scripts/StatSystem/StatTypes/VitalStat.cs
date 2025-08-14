using System;
using UnityEngine;

namespace AdventurerVillage.StatSystem
{
    [Serializable]
    public class VitalStat : DerivedStats
    {
        #region Constructors

        public VitalStat()
        {
            
        }
        public VitalStat(Attribute derivedAttribute, float attributeMultiplier) : base(derivedAttribute, attributeMultiplier)
        {
            _currentValue = value.Value;
        }

        public VitalStat(Attribute derivedAttribute, float attributeMultiplier, float baseValue) : base(derivedAttribute,
            attributeMultiplier, baseValue)
        {
            _currentValue = value.Value;
        }

        #endregion

        private float _currentValue;
        public float CurrentValue => _currentValue;

        public Action OnVitalCurrentValueChanged;

        public void Recover(float recoverAmount)
        {
            _currentValue = Mathf.Clamp(_currentValue + recoverAmount, 0, value.Value);
        }

        public void Reduce(float reduceAmount)
        {
            if (reduceAmount < 0) return;
            _currentValue = Mathf.Clamp(_currentValue - reduceAmount, 0, value.Value);
            OnVitalCurrentValueChanged?.Invoke();
        }

        public bool HasEnoughValue(float requiredValue)
        {
            return requiredValue <= _currentValue;
        }
    }
}