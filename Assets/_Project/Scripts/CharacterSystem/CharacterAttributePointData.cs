using System;
using System.Linq;
using AdventurerVillage.StatSystem;
using UnityEngine;

namespace AdventurerVillage.CharacterSystem
{
    [CreateAssetMenu(fileName = "CharacterAttributePointData", menuName = "Adventurer Village/Character System/Character Attribute Point Data")]
    public class CharacterAttributePointData : ScriptableObject
    {
        [SerializeField] private CharacterAttributePoint[] attributePoints;

        public float GetAttributePointValue(CharacterAttributeTypes attributeType)
        {
            return attributePoints.First(info => info.attributeType == attributeType).point;
        }
    }

    [Serializable]
    public struct CharacterAttributePoint
    {
        public CharacterAttributeTypes attributeType;
        public float point;
    }
}