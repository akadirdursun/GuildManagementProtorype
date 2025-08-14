using System;
using System.Collections.Generic;
using System.Linq;
using AdventurerVillage.HexagonGridSystem;
using AdventurerVillage.SaveSystem;
using UnityEngine;

namespace AdventurerVillage.BuildingSystem
{
    [CreateAssetMenu(fileName = "BuildingSaveData", menuName = "Adventurer Village/Building System/Building Save Data")]
    public class BuildingSaveData : SavableScriptableObject
    {
        [SerializeField] private HexMapData hexMapData;
        private List<BuildingInfo> _buildingInfos;
        private List<HexCoordinates> _buildableAreaInfos;

        private HashSet<HexCoordinates> _claimableBuildingPlaces = new();
        public BuildingInfo[] BuildingInfos => _buildingInfos.ToArray();

        public int TotalClaimedLandCount => _buildingInfos.Count + _buildableAreaInfos.Count;
        public Action<HexCoordinates> OnNewCoordinateAddedAsClaimable;
        public Action<HexCoordinates> SetCoordinateAsClaimed;
        public Action OnNewLandClaimed;

        public void Initialize()
        {
            foreach (var buildingInfo in _buildingInfos)
            {
                BuildingSpawner.Instance.SpawnBuilding(buildingInfo);
            }

            foreach (var buildableAreaCoordinate in _buildableAreaInfos)
            {
                SetCoordinateAsClaimed.Invoke(buildableAreaCoordinate);
            }

            CheckPurchasableBuildingPlaces();
        }

        public void NewBuildingPlaced(BuildingInfo buildingInfo)
        {
            if (_buildableAreaInfos.All(coordinates => coordinates != buildingInfo.Coordinates)) return;
            _buildableAreaInfos.Remove(buildingInfo.Coordinates);
            _buildingInfos.Add(buildingInfo);
            OnNewBuilderPlaced(buildingInfo.Coordinates);
        }

        public void NewLandClaimed(HexCoordinates coordinates)
        {
            _buildableAreaInfos.Add(coordinates);
            SetCoordinateAsClaimed.Invoke(coordinates);
            OnNewLandClaimed?.Invoke();
        }

        public BuildingInfo GetBuildingInfo(string buildingId)
        {
            return _buildingInfos.First(info => info.BuildingID == buildingId);
        }

        private void CheckPurchasableBuildingPlaces()
        {
            _claimableBuildingPlaces = new HashSet<HexCoordinates>();
            var buildingPlaces = new List<HexCoordinates>();
            buildingPlaces.AddRange(_buildingInfos.Select(info => info.Coordinates));
            buildingPlaces.AddRange(_buildableAreaInfos);
            foreach (var coordinates in buildingPlaces)
            {
                var neighbors = coordinates.GetNeighbors();
                foreach (var neighbor in neighbors)
                {
                    if (_claimableBuildingPlaces.Contains(neighbor.Coordinates) ||
                        buildingPlaces.Any(savedCoordinates => savedCoordinates == neighbor.Coordinates) ||
                        !hexMapData.TryGetCell(neighbor.Coordinates, out var cell) ||
                        cell.HexType == HexType.Water) continue;
                    _claimableBuildingPlaces.Add(neighbor.Coordinates);
                    OnNewCoordinateAddedAsClaimable.Invoke(neighbor.Coordinates);
                }
            }
        }

        private void OnNewBuilderPlaced(HexCoordinates newBuildingCoordinates)
        {
            if (_claimableBuildingPlaces.Contains(newBuildingCoordinates))
                _claimableBuildingPlaces.Remove(newBuildingCoordinates);
            var neighbors = newBuildingCoordinates.GetNeighbors();
            foreach (var neighbor in neighbors)
            {
                if (_claimableBuildingPlaces.Contains(neighbor.Coordinates) ||
                    _buildingInfos.Any(info => info.Coordinates == neighbor.Coordinates) ||
                    _buildableAreaInfos.Any(coordinates => coordinates == neighbor.Coordinates) ||
                    !hexMapData.TryGetCell(neighbor.Coordinates, out var cell) ||
                    cell.HexType == HexType.Water) continue;
                _claimableBuildingPlaces.Add(neighbor.Coordinates);
                OnNewCoordinateAddedAsClaimable.Invoke(neighbor.Coordinates);
            }
        }

        #region Save Methods

        public override void Save()
        {
            //ES3.Save("buildingInfos", _buildingInfos);
            //ES3.Save("buildableAreaInfos", _buildableAreaInfos);
        }


        public override void Load()
        {
            //_buildingInfos = ES3.Load("buildingInfos", new List<BuildingInfo>());
            //_buildableAreaInfos = ES3.Load("buildableAreaInfos", new List<HexCoordinates>());
        }

        public override void Reset()
        {
            _buildingInfos = new();
            _buildableAreaInfos = new() { new HexCoordinates(0, 0) };
        }

        #endregion
    }
}