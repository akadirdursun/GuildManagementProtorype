using UnityEngine;

namespace AdventurerVillage.GateSystem
{
    [CreateAssetMenu(fileName = "SelectedGateData", menuName = "Adventurer Village/Gate System/Selected Gate Data")]
    public class SelectedGateData : ScriptableObject
    {
        private GateInfo _selectedGate;

        public GateInfo SelectedGate => _selectedGate;

        public void SetSelectedGate(GateInfo selectedGate)
        {
            _selectedGate = selectedGate;
        }
    }
}