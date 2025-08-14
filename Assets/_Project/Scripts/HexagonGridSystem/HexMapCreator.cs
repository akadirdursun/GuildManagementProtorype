using AKD.Common.GameEventSystem;
using UnityEngine;

namespace AdventurerVillage.HexagonGridSystem
{
    public class HexMapCreator : MonoBehaviour
    {
        [SerializeField] private HexMapData hexMapData;
        [SerializeField] private HexGridSettings hexGridSettings;
        [SerializeField] private Transform gridCellHolder;
        [SerializeField] private GameEvent onMapPlacementCompleted;

#if UNITY_EDITOR
        [ContextMenu("Generate Hex Map")]
        private void CreateMap()
        {
            hexMapData.ClearCells();
            HexCellConfig[] cellConfigs = hexMapData.CreateRandomMapConfig();
        }
#endif

        private void PlaceMap()
        {
            var cellConfigs = hexMapData.MapConfig;
            hexMapData.ClearCells();
            foreach (var cellConfig in cellConfigs)
            {
                if (cellConfig.hexType == HexType.Water) continue;
                var cell = hexMapData.CreateCell(cellConfig);
                var coord = cellConfig.coordinates;
                // Create cell
                cell.transform.SetParent(gridCellHolder);
                var position = coord.ToWorldPosition();
                cell.transform.localPosition = position;
                cell.gameObject.name += $"{coord.ToString()}";
                cell.Initialize(coord);
            }

            onMapPlacementCompleted.Invoke();
        }

        #region MonoBehaviour

        private void Start()
        {
            PlaceMap();
        }

        #endregion
    }
}