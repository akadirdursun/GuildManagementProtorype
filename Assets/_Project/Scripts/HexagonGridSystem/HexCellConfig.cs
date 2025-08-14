using System;

namespace AdventurerVillage.HexagonGridSystem
{
    [Serializable]
    public struct HexCellConfig
    {
        public HexType hexType;
        public HexCoordinates coordinates;
    }
}