using UnityEngine;

namespace AdventurerVillage.HexagonGridSystem
{
    [CreateAssetMenu(fileName = "HexGridSettings",
        menuName = "Adventurer Village/Hexagon Grid System/Hex Grid Settings")]
    public class HexGridSettings : ScriptableObject
    {
        [SerializeField] private int width = 5;
        [SerializeField] private int height = 5;
        [SerializeField, Range(0.01f, 1f)] private float noiseScale = 0.1f;
        [SerializeField, Range(0f, 1f)] private float waterThreshold = 0.4f;
        [SerializeField, Range(0f, 1f)] private float desertThreshold = 0.5f;
        [SerializeField, Range(0f, 1f)] private float grassThreshold = 0.7f;
        [SerializeField, Range(0f, 1f)] private float forestThreshold = 0.85f;
        [SerializeField] private int cityCount = 1;
        [SerializeField] private float minCityDistance = 10f;
        [SerializeField] private float maxCityDistanceFromCenter = 10f;

        public const float OuterRadius = 10f;
        public int Width => width;
        public int Height => height;
        public float NoiseScale => noiseScale;
        public float WaterThreshold => waterThreshold;
        public float DesertThreshold => desertThreshold;
        public float GrassThreshold => grassThreshold;
        public float ForestThreshold => forestThreshold;
        public int CityCount => cityCount;
        public float MinCityDistance => minCityDistance;
        public float MaxCityDistanceFromCenter => maxCityDistanceFromCenter;
    }
}