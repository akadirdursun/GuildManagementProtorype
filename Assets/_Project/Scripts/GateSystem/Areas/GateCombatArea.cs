using System;
using AdventurerVillage.CharacterSystem;
using AdventurerVillage.GateSystem.Enums;
using AdventurerVillage.GateSystem.Interfaces;

namespace AdventurerVillage.GateSystem
{
    [Serializable]
    public class GateCombatArea : IGateArea
    {
        #region Constructor

        public GateCombatArea(CharacterInfo[] enemies)
        {
            Enemies = enemies;
        }

        #endregion

        public GateAreaTypes GateAreaType => GateAreaTypes.CombatArea;
        public CharacterInfo[] Enemies { get; private set; }
    }
}