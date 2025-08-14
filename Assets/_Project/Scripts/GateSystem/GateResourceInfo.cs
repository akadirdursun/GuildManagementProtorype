using System;
using AdventurerVillage.ResourceSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AdventurerVillage.GateSystem
{
    [Serializable]
    public struct GateResourceInfo
    {
        [SerializeField] private ResourceData resourceData;
        [SerializeField] private int minAmount;
        [SerializeField] private int maxAmount;

        public ResourceData ResourceData => resourceData;
        public int Amount => Random.Range(minAmount, maxAmount + 1);
    }
}