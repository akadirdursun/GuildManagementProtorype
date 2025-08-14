using System;
using AdventurerVillage.Utilities;
using UnityEngine;

namespace AdventurerVillage.ItemSystem
{
    public abstract class Item : ScriptableObject
    {
        [SerializeField, ReadOnly] protected string id;
        [SerializeField] protected string itemName;
        [SerializeField] protected string description;
        [SerializeField] protected int maxStackSize = 1;
        [SerializeField] protected Sprite icon;

        public string Id => id;
        public string Name => itemName;
        public string Description => description;
        public int MaxStackSize => maxStackSize;
        public Sprite Icon => icon;

        #region SciptableObject Methods

        private void Awake()
        {
            if (string.IsNullOrEmpty(id))
            {
                SetItemId();
            }
        }

#if UNITY_EDITOR
        private void SetItemId()
        {
            id = Guid.NewGuid().ToString();
        }
        
#endif

        #endregion
    }
}