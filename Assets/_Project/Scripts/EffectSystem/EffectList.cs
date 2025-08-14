using System;
using System.Collections.Generic;
using UnityEngine;

namespace AdventurerVillage.EffectSystem
{
    [Serializable]
    public class EffectList
    {
        [SerializeReference] private List<Effect> effects = new ();
        
        public List<Effect> Effects => effects;
    }
}