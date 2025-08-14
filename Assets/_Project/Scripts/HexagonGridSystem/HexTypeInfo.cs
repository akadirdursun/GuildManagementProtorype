using System;
using System.Linq;
using UnityEngine;

namespace AdventurerVillage.HexagonGridSystem
{
    [Serializable]
    public struct HexTypeInfo
    {
        public HexType hexType;
        [SerializeField] private HexCellPrefabInfo[] cellPrefabs;

        public HexCell GetCellPrefabs(float distance)
        {
            return cellPrefabs.Last(info => info.distance <= distance).cellPrefab;
        }
    }

    [Serializable]
    public struct HexCellPrefabInfo
    {
        public float distance;
        public HexCell cellPrefab;
    }
}