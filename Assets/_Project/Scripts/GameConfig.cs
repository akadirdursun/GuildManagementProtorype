using AdventurerVillage.HexagonGridSystem;

namespace AdventurerVillage
{
    public static class GameConfig
    {
        private static readonly GameInfo GameInfo = new();
        public static HexCoordinates CityCoordinates = new HexCoordinates(0, 0);
        public static float BaseCraftingQualityValue = 1f;
    }
}