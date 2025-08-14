using System;
using AdventurerVillage.EffectSystem;
using AdventurerVillage.LevelSystem;
using UnityEngine;

namespace AdventurerVillage.ItemSystem.Equipments
{
    [Serializable/*, ReadOnly*/]
    public class EquipmentInfo : ItemInfo
    {
        #region Constructor

        public EquipmentInfo()
        {
        }

        public EquipmentInfo(
            string id,
            string name,
            Grade grade,
            EquipmentTypes equipmentType,
            float itemDurability,
            StatEffect[] combatStatEffects) : base(id, name, grade, 1, false, 1)
        {
            _equipmentType = equipmentType;
            _itemDurability = itemDurability;
            _combatStatEffects = combatStatEffects;
        }

        #endregion

        private EquipmentTypes _equipmentType;
        private float _itemDurability;
        private StatEffect[] _combatStatEffects;

        public override Sprite Icon => ItemIconManager.Instance.GetEquipmentIcon(Id);
        public EquipmentTypes EquipmentType => _equipmentType;
        public float ItemDurability => _itemDurability;
        public StatEffect[] CombatStatEffects => _combatStatEffects;
        public Action OnItemBrake;

        public void ReduceDurability(float value)
        {
            _itemDurability -= value;
            if (ItemDurability <= 0)
                OnItemBrake?.Invoke();
        }

        public override void SetItemAmount(int newAmount)
        {
            Debug.LogError("You cannot change the item amount of an equipment.");
        }

        protected override string GetItemEffectInfo()
        {
            var log = "";

            foreach (var combatStatEffect in CombatStatEffects)
            {
                log += $"• {combatStatEffect.GetEffectInfo()}\n";
            }

            return log;
        }

        protected override string GetItemDescription()
        {
            return $"Just a {_equipmentType}";
        }
    }
}