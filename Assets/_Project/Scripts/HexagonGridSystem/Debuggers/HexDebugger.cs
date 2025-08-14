using AdventurerVillage.HexagonGridSystem.Pathfinding;
using UnityEngine;

namespace AdventurerVillage.HexagonGridSystem.Debuggers
{
    public class HexDebugger : MonoBehaviour
    {
        [SerializeField] private HexCoordDebugger hexCoordDebugger;
        [SerializeField] private HexAStarDebugger hexAStarDebugger;
        public void Initialize(HexCoordinates coordinates)
        {
            hexCoordDebugger.Initialize(coordinates);
        }

        public void InitPathDebugger(HexNode node, Color color)
        {
            hexAStarDebugger.Initialize(node, color);
            hexAStarDebugger.gameObject.SetActive(true);
        }

        public void ClearPathDebugger()
        {
            hexAStarDebugger.gameObject.SetActive(false);
        }
    }
}
