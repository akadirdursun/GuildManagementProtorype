using UnityEngine;

namespace AdventurerVillage.HexagonGridSystem
{
    [CreateAssetMenu(fileName = "SelectedHexCellData", menuName = "Adventurer Village/Hexagon Grid System/Selected Hex Cell Data")]
    public class SelectedHexCellData : ScriptableObject
    {
        private HexCell _selectedHexCell;

        public HexCell SelectedHexCell => _selectedHexCell;

        public void SelectHexCell(HexCell selectedHexCell)
        {
            ClearSelectedHexCell();
            _selectedHexCell = selectedHexCell;
            _selectedHexCell.Select();
        }

        public void ClearSelectedHexCell()
        {
            if (_selectedHexCell == null) return;
            _selectedHexCell.Deselect();
        }
    }
}