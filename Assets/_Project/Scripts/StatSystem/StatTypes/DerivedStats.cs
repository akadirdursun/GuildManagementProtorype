using System;

namespace AdventurerVillage.StatSystem
{
    [Serializable]
    public class DerivedStats : Stat
    {
        #region Constructors

        public DerivedStats()
        {
            
        }
        
        public DerivedStats(Attribute derivedAttribute, float attributeMultiplier) : base()
        {
            _derivedAttribute = derivedAttribute;
            _attributeMultiplier = attributeMultiplier;
            Calculate();
            _derivedAttribute.OnCharacterStatChanged += Calculate;
        }

        public DerivedStats(Attribute derivedAttribute, float attributeMultiplier, float baseValue) : base(baseValue)
        {
            _derivedAttribute = derivedAttribute;
            _attributeMultiplier = attributeMultiplier;
            Calculate();
            _derivedAttribute.OnCharacterStatChanged += Calculate;
        }

        public DerivedStats(Attribute derivedAttribute, float attributeMultiplier, float baseValue, float maxValue) : base(baseValue,
            maxValue)
        {
            _derivedAttribute = derivedAttribute;
            _attributeMultiplier = attributeMultiplier;
            Calculate();
            _derivedAttribute.OnCharacterStatChanged += Calculate;
        }

        #endregion

        private Attribute _derivedAttribute;
        private float _attributeMultiplier;

        private void Calculate()
        {
            var baseValue = _derivedAttribute.Level * _attributeMultiplier;
            value.ChangeBaseValue(baseValue);
        }
    }
}