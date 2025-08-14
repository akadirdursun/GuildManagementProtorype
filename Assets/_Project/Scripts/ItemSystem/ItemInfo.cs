using System;
using AdventurerVillage.LevelSystem;
using UnityEngine;

namespace AdventurerVillage.ItemSystem
{
    [Serializable]
    public abstract class ItemInfo
    {
        #region Constructors

        protected ItemInfo()
        {
        }

        protected ItemInfo(string id, string name, Grade grade, int itemAmount, bool isStackable, int maxStackSize)
        {
            _id = id;
            _name = name;
            _grade = grade;
            _itemAmount = itemAmount;
            _isStackable = isStackable;
            _maxStackSize = maxStackSize;
        }

        #endregion

        private string _id;
        private string _name;
        private Grade _grade;
        private int _itemAmount;
        private bool _isStackable;
        private int _maxStackSize;
        public string Id => _id;
        public string Name => _name;
        public abstract Sprite Icon { get; }
        public Grade Grade => _grade;
        public int ItemAmount => _itemAmount;
        public string ItemEffectInfo => GetItemEffectInfo();
        public string ItemDescription => GetItemDescription();
        public bool IsStackable => _isStackable;
        public int MaxStackSize => _maxStackSize;

        public virtual void SetItemAmount(int newAmount)
        {
            _itemAmount = newAmount;
        }

        protected abstract string GetItemEffectInfo();
        protected abstract string GetItemDescription();
    }
}