using UnityEngine;

namespace AdventurerVillage.HexagonGridSystem
{
    public static class HexCoordinateExtensions
    {
        public static HexNeighbor[] GetNeighbors(this HexCoordinates coordinates)
        {
            var neighbors = new HexNeighbor[]
            {
                new(HexDirections.NorthEast, coordinates + new Vector2Int(1, -1)),
                new(HexDirections.East, coordinates + new Vector2Int(1, 0)),
                new(HexDirections.SouthEast, coordinates + new Vector2Int(0, 1)),
                new(HexDirections.SouthWest, coordinates + new Vector2Int(-1, 1)),
                new(HexDirections.West, coordinates + new Vector2Int(-1, 0)),
                new(HexDirections.NorthWest, coordinates + new Vector2Int(0, -1)),
            };

            return neighbors;
        }
        
        public static Vector3 ToWorldPosition(this HexCoordinates coordinates)
        {
            var outerRadius = HexGridSettings.OuterRadius;
            // Calculate the inner radius from the outer radius
            float innerRadius = HexUtility.GetInnerRadius();

            // Calculate the horizontal and vertical offsets
            float xOffset = (coordinates.X + coordinates.Z / 2f) * (innerRadius * 2f);
            float zOffset = coordinates.Z * (outerRadius * 1.5f);

            // Return the world position
            return new Vector3(xOffset, 0f, zOffset);
        }
        
        public static int HexDistanceTo(this HexCoordinates a, HexCoordinates b)
        {
            int dx = Mathf.Abs(a.X - b.X);
            int dz = Mathf.Abs(a.Z - b.Z);
            int dy = Mathf.Abs(a.Y - b.Y);
            return Mathf.Max(dx, Mathf.Max(dy, dz));
        }
    }
}