using System;

namespace AdventurerVillage.HexagonGridSystem
{
    [Serializable]
    public struct HexNeighbor
    {
        public HexNeighbor(HexDirections direction, HexCoordinates coordinates)
        {
            Direction = direction;
            Coordinates = coordinates;
        }
        
        public HexDirections Direction { get; private set; }
        public HexCoordinates Coordinates{ get; private set; }
    }
}