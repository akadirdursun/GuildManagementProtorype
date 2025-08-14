using AdventurerVillage.HexagonGridSystem;

namespace AdventurerVillage.Utilities
{
    public static class HexDirectionsExtensions
    {
        public static HexDirections Opposite(this HexDirections direction)
        {
            return (int)direction < 3 ? direction + 3 : direction - 3;
        }
    }
}