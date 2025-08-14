using System;
using AdventurerVillage.Utilities;
using UnityEngine;

namespace AdventurerVillage.ClassSystem
{
    public abstract class BaseClass : ScriptableObject
    {
        [SerializeField, ReadOnly] private string id;
        [SerializeField] private string className;
        [SerializeField] private Sprite classIcon;
        
        public string Id => id;
        public string ClassName => className;
        public Sprite ClassIcon => classIcon;
        
        private void Awake()
        {
            if (string.IsNullOrEmpty(id))
            {
                id = Guid.NewGuid().ToString();
            }

            if (string.IsNullOrEmpty(className))
            {
                className = name;
            }
        }
    }
}