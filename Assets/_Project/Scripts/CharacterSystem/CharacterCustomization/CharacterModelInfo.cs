using System;
using UnityEngine;

namespace AdventurerVillage.CharacterSystem.CharacterCustomization
{
    [Serializable]
    public struct CharacterModelInfo
    {
        public Genders gender;
        public Color skinColor;
        public Color hairColor;
        public int hairMeshIndex;
        public int headMeshIndex;
        public int eyebrowsMeshIndex;
        public int facialHairMeshIndex;
    }
}