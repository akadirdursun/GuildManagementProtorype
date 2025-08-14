using UnityEngine;

namespace AdventurerVillage.CombatSystem
{
    [CreateAssetMenu(fileName = "CombatPreviewData", menuName = "Adventurer Village/Combat System/Combat Preview Data")]
    public class CombatPreviewData : ScriptableObject
    {
        private CombatBlackboard _combatBlackboard;
        public CombatBlackboard CombatBlackboard => _combatBlackboard;

        public void SetCombatBlackboard(CombatBlackboard combatBlackboard)
        {
            _combatBlackboard = combatBlackboard;
        }
    }
}