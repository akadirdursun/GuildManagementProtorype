using System;
using System.Collections.Generic;
using System.Linq;
using AdventurerVillage.EffectSystem;
using AdventurerVillage.EffectSystem.Enums;
using UnityEngine;

namespace AdventurerVillage.StatSystem
{
    [Serializable]
    public class EffectedFloat : IEffected
    {
        #region Constructors

        public EffectedFloat(float baseValue, float maxValue = float.MaxValue)
        {
            this.baseValue = baseValue;
            _maxValue = maxValue;
            CalculateValue();
        }

        #endregion

        [SerializeField] private float value;
        [SerializeField] private float baseValue;
        [SerializeField] private float totalEffectAddition;
        [SerializeField] private float totalEffectMultiplier;

        private List<EffectInfo> _effects = new();
        private float _maxValue;

        public float Value => value;

        public void ChangeBaseValue(float newBaseValue)
        {
            baseValue = newBaseValue;
            CalculateValue();
        }

        private void CalculateValue()
        {
            var additionalValue = baseValue * totalEffectMultiplier + totalEffectAddition;
            value = Mathf.Clamp(baseValue + additionalValue, 0f, _maxValue);
        }

        public void AddEffect(EffectInfo effectInfo)
        {
            var effect = _effects.FirstOrDefault(e => e.effectSource == effectInfo.effectSource);
            if (effect == null)
            {
                _effects.Add(effectInfo);
                AddEffectValue(effectInfo.effectValue);
                return;
            }

            var previousEffectValue = effect.effectValue;
            var diff = previousEffectValue - effectInfo.effectValue;
            effect.effectValue = effectInfo.effectValue;
            AddEffectValue(diff);

            void AddEffectValue(float effectValue)
            {
                switch (effectInfo.effectType)
                {
                    case EffectType.Addition:
                        totalEffectAddition += effectValue;
                        break;
                    case EffectType.Multiplication:
                        totalEffectMultiplier += effectValue;
                        break;
                }

                CalculateValue();
            }
        }

        public float GetEffectChange(EffectInfo newEffect)
        {
            var oldEffect = _effects.FirstOrDefault(effect => effect.effectSource == newEffect.effectSource);
            if (oldEffect == null)
            {
                if (newEffect.effectType == EffectType.Multiplication)
                    return baseValue * newEffect.effectValue;

                return newEffect.effectValue;
            }

            if (newEffect.effectType == EffectType.Multiplication)
                return baseValue * (oldEffect.effectValue - newEffect.effectValue);
            return newEffect.effectValue-oldEffect.effectValue;
        }

        public void RemoveEffect(string effectSource)
        {
            var effect = _effects.FirstOrDefault(e => e.effectSource == effectSource);
            if (effect == null) return;
            RemoveEffectValue();
            _effects.Remove(effect);

            void RemoveEffectValue()
            {
                switch (effect.effectType)
                {
                    case EffectType.Addition:
                        totalEffectAddition -= effect.effectValue;
                        break;
                    case EffectType.Multiplication:
                        totalEffectMultiplier -= effect.effectValue;
                        break;
                }

                CalculateValue();
            }
        }
    }
}