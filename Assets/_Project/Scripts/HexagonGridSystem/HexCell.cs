using AdventurerVillage.Utilities;
using AdventurerVillage.VFXSystems;
using UnityEngine;

namespace AdventurerVillage.HexagonGridSystem
{
    public class HexCell : MonoBehaviour
    {
        [SerializeField] private HexType hexType;
        [SerializeField, ReadOnly] private HexCoordinates coordinates;
        [SerializeField] private float terrainDifficulty;
        [SerializeField] private HexCellSizeManager hexCellSizeManager;
        [SerializeField] private HexLandManager hexLandManager;
        [SerializeField] private GlowHighlight glowHighlight;
        
        public HexType HexType => hexType;
        public HexCoordinates Coordinates => coordinates;
        public float TerrainDifficulty => terrainDifficulty;
        public HexLandManager HexLandManager => hexLandManager;

        public void Initialize(HexCoordinates coord)
        {
            coordinates = coord;
            hexCellSizeManager.Initialize();
        }
        
        public void Select()
        {
            glowHighlight.EnableGlow();
            hexLandManager.OnSelect();
        }

        public void Deselect()
        {
            glowHighlight.DisableGlow();
            hexLandManager.OnDeselect();
        }
    }
}