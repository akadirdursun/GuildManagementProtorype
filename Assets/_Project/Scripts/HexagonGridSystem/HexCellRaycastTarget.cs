using UnityEngine;

namespace AdventurerVillage.HexagonGridSystem
{
    public class HexCellRaycastTarget : MonoBehaviour
    {
        [SerializeField] private HexCell hexCell;

        public HexCell HexCell => hexCell;
    }
}