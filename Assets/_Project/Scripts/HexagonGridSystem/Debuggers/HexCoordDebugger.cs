using TMPro;
using UnityEngine;

namespace AdventurerVillage.HexagonGridSystem.Debuggers
{
    public class HexCoordDebugger : MonoBehaviour
    {
        [SerializeField] private TMP_Text label;

        public void Initialize(HexCoordinates coordinates)
        {
            label.text = coordinates.ToString();
        }
    }
}