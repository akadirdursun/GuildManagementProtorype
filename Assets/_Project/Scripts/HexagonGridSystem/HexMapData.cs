using System.Collections.Generic;
using System.Linq;
using AdventurerVillage.HexagonGridSystem.Algorithms;
using AdventurerVillage.SaveSystem;
using UnityEngine;

namespace AdventurerVillage.HexagonGridSystem
{
    [CreateAssetMenu(fileName = "HexMapData",
        menuName = "Adventurer Village/Hexagon Grid System/Hex Map Data")]
    public class HexMapData : SavableScriptableObject
    {
        [SerializeField] private HexGridSettings hexGridSettings;
        [SerializeField] private HexTypeInfo[] hexTypes;
        private HexCellConfig[] _mapConfig;
        private Dictionary<HexCoordinates, HexCell> _hexCells = new();

        public List<HexCell> HexCells => _hexCells.Values.ToList();
        public HexCellConfig[] MapConfig
        {
            get
            {
                if (_mapConfig == null || _mapConfig.Length == 0)
                    CreateRandomMapConfig();

                return _mapConfig;
            }
        }

        public HexCellConfig[] CreateRandomMapConfig()
        {
            _mapConfig = RandomMapGenerator.CreateRandomMap(hexGridSettings).ToArray();
            return _mapConfig;
        }

        public HexCell CreateCell(HexCellConfig hexCellConfig)
        {
            var baseCoord = GameConfig.CityCoordinates;
            var hexCellPrefab = GetBiomePrefab(hexCellConfig.hexType, hexCellConfig.coordinates.HexDistanceTo(baseCoord));
            var cell = Instantiate(hexCellPrefab);
            _hexCells.Add(hexCellConfig.coordinates, cell);
            return cell;
        }

        public void ClearCells()
        {
            foreach (var hexCell in _hexCells.Values)
            {
                if (hexCell != null)
                    Destroy(hexCell.gameObject);
            }

            _hexCells.Clear();
        }

        private HexCell GetBiomePrefab(HexType hexType, float distance)
        {
            return hexTypes.First(c => c.hexType == hexType).GetCellPrefabs(distance);
        }

        public bool TryGetCell(HexCoordinates coordinates, out HexCell cell)
        {
            return _hexCells.TryGetValue(coordinates, out cell);
        }

        public bool TryGetCityCell(out HexCell cell)
        {
            return TryGetCell(GameConfig.CityCoordinates, out cell);
        }

        public List<HexCell> GetCoordinatesInDistanceToCity(int minDistance, int maxDistance)
        {
            var cellCoordinates = new List<HexCell>();
            HexCellConfig city = new(); //TODO: Set cityCellConfig
            foreach (var cell in _hexCells)
            {
                if (cell.Value.HexType == HexType.Water) continue;
                var minDist = int.MaxValue;
                var distance = cell.Key.HexDistanceTo(city.coordinates);
                if (distance < minDist)
                    minDist = distance;


                if (minDistance <= minDist && minDist <= maxDistance)
                    cellCoordinates.Add(cell.Value);
            }

            return cellCoordinates;
        }

        #region Sava System Methods

        public override void Save()
        {
            //ES3.Save("mapConfig", _mapConfig);
        }

        public override void Load()
        {
            //_mapConfig = ES3.Load<HexCellConfig[]>("mapConfig");
        }

        public override void Reset()
        {
            _mapConfig = null;
        }

        #endregion
    }
}