using System;
using AdventurerVillage.EffectSystem.Enums;

namespace AdventurerVillage.EffectSystem
{
    [Serializable]
    public class EffectInfo
    {
        public EffectType effectType;
        public string effectSource;
        public float effectValue;
    }
}