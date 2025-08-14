using System;
using AdventurerVillage.StatSystem;

namespace AdventurerVillage.CharacterSystem
{
    [Serializable]
    public class AttributeOrder
    {
        public CharacterAttributeTypes[] attributeTypes = new[]
        {
            CharacterAttributeTypes.Strength,
            CharacterAttributeTypes.Constitution,
            CharacterAttributeTypes.Agility,
            CharacterAttributeTypes.Intelligence,
            CharacterAttributeTypes.Magic
        };
    }
}