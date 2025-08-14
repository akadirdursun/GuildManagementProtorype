using System;
using ADK.Common.ObjectPooling;
using AdventurerVillage.EffectSystem;
using AdventurerVillage.StatSystem;
using AdventurerVillage.UI;
using UnityEngine;

namespace AdventurerVillage.CraftingSystem.UI
{
    public class CraftedItemStatArea : MonoBehaviour
    {
        [SerializeField] private ValueView valueViewPrefab;
        [SerializeField] private Color backgroundColor;

        private IObjectPool<ValueView> _statViewPool;

        public void Initialize(StatEffect[] combatStatEffects)
        {
            _statViewPool.ReleaseAll();
            foreach (var statEffect in combatStatEffects)
            {
                var statView = _statViewPool.Get();
                statView.Initialize(GetTitle(statEffect.StatType), $"{statEffect.EffectValue:N2}", backgroundColor);
            }
        }

        private string GetTitle(StatTypes statType)
        {
            return statType switch
            {
                StatTypes.Health => "Health",
                StatTypes.HealthRegen => "Health Regen",
                StatTypes.Stamina => "Stamina",
                StatTypes.StaminaRegen => "Stamina Regen",
                StatTypes.Mana => "Mana",
                StatTypes.ManaRegen => "Mana Regen",
                StatTypes.Damage => "Damage",
                StatTypes.DamageReductionFlat => "DamageReduction",
                StatTypes.DamageReductionPercentage => "DamageReductionPercent",
                StatTypes.DodgeChance => "Dodge Chance",
                StatTypes.CriticalHitChance => "Critical Hit Chance",
                StatTypes.DamageReflect => "Damage Reflect",
                StatTypes.CounterChance => "Counter Chance",
                StatTypes.AttackSpeed => "Attack Speed",
                StatTypes.ManaCostReduction => "Mana Cost Reduction",
                StatTypes.CraftQuality => "Craft Quality",
                StatTypes.CraftProductivity => "Craft Productivity",
                _ => throw new ArgumentOutOfRangeException(nameof(statType), statType, null)
            };
        }

        #region MonoBehaviour Methods

        private void Start()
        {
            _statViewPool = new ObjectPool<ValueView>(valueViewPrefab, 2, OnCreate, OnGet, OnRelease);
        }

        #endregion

        #region Pool Methods

        private void OnCreate(ValueView view, ObjectPool<ValueView> pool)
        {
            view.transform.SetParent(transform, false);
            view.gameObject.SetActive(false);
        }

        private void OnGet(ValueView view)
        {
            view.gameObject.SetActive(true);
        }

        private void OnRelease(ValueView view)
        {
            view.gameObject.SetActive(false);
        }

        #endregion
    }
}