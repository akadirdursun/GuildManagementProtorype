using System;
using AdventurerVillage.GateSystem.Enums;
using AdventurerVillage.GateSystem.Interfaces;
using AdventurerVillage.ResourceSystem;

namespace AdventurerVillage.GateSystem
{
    [Serializable]
    public class GateResourceArea : IGateArea
    {
        #region Constructor

        public GateResourceArea(string resourceTypeId, int resourceAmount)
        {
            ResourceTypeId = resourceTypeId;
            ResourceAmount = resourceAmount;
        }

        #endregion

        public GateAreaTypes GateAreaType => GateAreaTypes.ResourceArea;
        public string ResourceTypeId { get; private set; }
        public int ResourceAmount { get; set; }
    }
}