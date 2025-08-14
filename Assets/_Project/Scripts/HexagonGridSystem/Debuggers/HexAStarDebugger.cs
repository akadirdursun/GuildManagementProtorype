using AdventurerVillage.HexagonGridSystem.Pathfinding;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.HexagonGridSystem.Debuggers
{
    public class HexAStarDebugger : MonoBehaviour
    {
        [SerializeField] private Image backgroundImage;
        [SerializeField] private TMP_Text difficulty;
        [SerializeField] private TMP_Text g;
        [SerializeField] private TMP_Text h;
        [SerializeField] private TMP_Text f;
        
        public void Initialize(HexNode node, Color color)
        {
            backgroundImage.color = color;
            difficulty.text = $"Dif:{node.TerrainDifficulty}";
            g.text = $"G:{node.G}";
            h.text = $"H:{node.H}";
            f.text = $"F:{node.F}";
        }
    }
}
