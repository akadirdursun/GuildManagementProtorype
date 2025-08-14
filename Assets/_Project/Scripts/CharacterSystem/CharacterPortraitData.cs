using System;

namespace AdventurerVillage.CharacterSystem
{
    [Serializable]
    public struct CharacterPortraitData
    {
        public byte[] portraitData;
        public int portraitWidth;
        public int portraitHeight;
    }
}