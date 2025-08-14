using System;
using AdventurerVillage.EffectSystem;
using UnityEngine;

namespace AdventurerVillage.StatSystem
{
    [Serializable]
    public class Stat
    {
        #region Constructors

        public Stat()
        {
            value = new EffectedFloat(0f);
        }

        public Stat(float baseValue)
        {
            value = new EffectedFloat(baseValue);
        }

        public Stat(float baseValue, float maxValue)
        {
            value = new EffectedFloat(baseValue, maxValue);
        }

        #endregion

        [SerializeField] protected EffectedFloat value;

        public Action OnStatValueChanged;

        public float Value => value.Value;

        public void AddEffect(EffectInfo effectInfo)
        {
            value.AddEffect(effectInfo);
            OnStatValueChanged?.Invoke();
        }

        public float GetEffectChange(EffectInfo newEffect)
        {
            return value.GetEffectChange(newEffect);
        }

        public void RemoveEffect(string effectSource)
        {
            value.RemoveEffect(effectSource);
            OnStatValueChanged?.Invoke();
        }
    }
}