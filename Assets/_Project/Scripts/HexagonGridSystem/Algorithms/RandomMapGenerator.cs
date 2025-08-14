using System.Collections.Generic;
using UnityEngine;

namespace AdventurerVillage.HexagonGridSystem.Algorithms
{
    public static class RandomMapGenerator
    {
        public static List<HexCellConfig> CreateRandomMap(HexGridSettings hexGridSettings)
        {
            var cells = new List<HexCellConfig>();
            var offsetX = Random.Range(0f, 999f);
            var offsetY = Random.Range(0f, 999f);
            var heightStart = -hexGridSettings.Height / 2;
            var heightEnd = hexGridSettings.Height + heightStart;
            var widthStart = -hexGridSettings.Width / 2;
            var widthEnd = hexGridSettings.Width + heightStart;
            for (int z = heightStart; z < heightEnd; z++)
            {
                for (int x = widthStart; x < widthEnd; x++)
                {
                    cells.Add(CreateCell(x, z, offsetX, offsetY, hexGridSettings));
                }
            }

            return cells;
        }

        private static HexCellConfig CreateCell(int x, int z, float offsetX, float offsetY,
            HexGridSettings hexGridSettings)
        {
            HexCoordinates coordinates = HexCoordinates.FromOffsetCoordinates(x, z);

            var hexType = GetHexType();

            return new HexCellConfig
            {
                coordinates = coordinates,
                hexType = hexType
            };

            HexType GetHexType()
            {
                // Compute a noise value based on the cell’s coordinate.
                var noiseScale = hexGridSettings.NoiseScale;
                float xNoiseScale = coordinates.X * noiseScale + offsetX;
                float yNoiseScale = coordinates.Y * noiseScale + offsetY;
                var noiseValue = Mathf.PerlinNoise(xNoiseScale, yNoiseScale);

                if (noiseValue < hexGridSettings.WaterThreshold)
                    return HexType.Water;

                if (noiseValue < hexGridSettings.DesertThreshold)
                    return HexType.Desert;

                if (noiseValue < hexGridSettings.GrassThreshold)
                    return HexType.Grass;

                if (noiseValue < hexGridSettings.ForestThreshold)
                    return HexType.Forest;

                return HexType.Rock;
            }
        }
    }
}