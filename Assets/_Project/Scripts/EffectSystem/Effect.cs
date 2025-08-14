using System;
using AdventurerVillage.EffectSystem.Enums;
using UnityEngine;
using CharacterInfo = AdventurerVillage.CharacterSystem.CharacterInfo;

namespace AdventurerVillage.EffectSystem
{
    [Serializable]
    public abstract class Effect : IEffect
    {
        [SerializeField] protected EffectType effectType;

        //[ListDrawerSettings(DraggableItems = false, ShowIndexLabels = true)]
        [SerializeField] protected float[] effectValuesByLevel;

        public abstract void ApplyEffect(CharacterInfo characterInfo, string effectSource);

        public float GetEffectValue(int effectLevel)
        {
            if (effectLevel >= effectValuesByLevel.Length) return effectValuesByLevel[^1];
            return effectValuesByLevel[effectLevel];
        }

        public abstract string GetEffectInfo(int level);
    }
}