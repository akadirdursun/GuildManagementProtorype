using System.Linq;
using ADK.Common.ObjectPooling;
using AdventurerVillage.ItemSystem.Equipments;
using AdventurerVillage.StatSystem;
using AdventurerVillage.StatSystem.UI;
using UnityEngine;

namespace AdventurerVillage.UI.CharacterPanel
{
    public class StatAreaController : MonoBehaviour
    {
        [SerializeField] private SelectedEquipmentSlotData selectedEquipmentSlotData;
        [SerializeField] private StatView statViewPrefab;
        [SerializeField] private RectTransform combatStatViewHolder;
        [SerializeField] private Color[] backgroundColorLoop;

        private IObjectPool<StatView> _statViewPool;
        private int _colorIndex;

        public void Initialize(CharacterStats characterStats)
        {
            _statViewPool.ReleaseAll();
            InitializeCombatStatView(characterStats.Damage, StatTypes.Damage, "Damage");
            InitializeCombatStatView(characterStats.DamageReductionFlat, StatTypes.DamageReductionFlat, "Damage Reduction");
            InitializeCombatStatView(characterStats.DamageReductionPercent, StatTypes.DamageReductionPercentage, "Damage Reduction Percent",
                "%");
            InitializeCombatStatView(characterStats.DodgeChance, StatTypes.DodgeChance, "Dodge Chance");
            InitializeCombatStatView(characterStats.CriticalHitChance, StatTypes.CriticalHitChance, "Critical Hit Chance", "%");
            InitializeCombatStatView(characterStats.DamageReflect, StatTypes.DamageReflect, "Damage Reflect", "%");
            InitializeCombatStatView(characterStats.CounterChance, StatTypes.CounterChance, "Counter Chance", "%");
            InitializeCombatStatView(characterStats.AttackSpeed, StatTypes.AttackSpeed, "Attack Speed", "%");
            InitializeCombatStatView(characterStats.ManaCostReduction, StatTypes.ManaCostReduction, "Mana Cost Reduction", "%");
            InitializeCombatStatView(characterStats.CraftQuality, StatTypes.CraftQuality, "Craft Quality", "%");
            InitializeCombatStatView(characterStats.CraftProductivity, StatTypes.CraftProductivity, "Craft Productivity");
        }

        private void ResetStatValues()
        {
            var activeStatViews = _statViewPool.ActiveObjectList;

            foreach (var statView in activeStatViews)
            {
                statView.UpdateValueText();
            }
        }

        private void InitializeCombatStatView(Stat stat, StatTypes statType, string title, string suffix = "")
        {
            var combatStatView = _statViewPool.Get();
            var backGroundColor = backgroundColorLoop[_colorIndex];
            _colorIndex = ++_colorIndex % backgroundColorLoop.Length;
            combatStatView.Initialize(stat, statType, title, suffix, backGroundColor);
        }

        private void OnEquipmentSlotSelected()
        {
            var selectedEquipmentSlot = selectedEquipmentSlotData.SelectedInventorySlot;
            var equipmentInfo = selectedEquipmentSlot.ItemInfo as EquipmentInfo;
            if (equipmentInfo == null) return;
            ResetStatValues();
            var activeStatViews = _statViewPool.ActiveObjectList;
            var source = $"{selectedEquipmentSlotData.SelectedEquipmentSlotType}";
            foreach (var statEffect in equipmentInfo.CombatStatEffects)
            {
                var statView = activeStatViews.FirstOrDefault(view => view.StatType == statEffect.StatType);
                if (statView == null) continue;
                var effectInfo = statEffect.GetEffectInfo(source);
                statView.UpdateValueText(effectInfo);
            }
        }

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            selectedEquipmentSlotData.OnEquipmentSlotSelected += OnEquipmentSlotSelected;
            selectedEquipmentSlotData.OnEquipmentSlotSelectionCleared += ResetStatValues;
        }

        private void Start()
        {
            _statViewPool =
                new ObjectPool<StatView>(statViewPrefab, 0, CreateSlot, GetSlot, ReleaseSlot);
        }

        private void OnDisable()
        {
            selectedEquipmentSlotData.OnEquipmentSlotSelected -= OnEquipmentSlotSelected;
            selectedEquipmentSlotData.OnEquipmentSlotSelectionCleared -= ResetStatValues;
        }

        #endregion

        #region Pool Methods

        private void CreateSlot(StatView statView, ObjectPool<StatView> pool)
        {
            statView.transform.SetParent(transform, false);
            statView.gameObject.SetActive(false);
        }

        private void GetSlot(StatView statView)
        {
            statView.transform.SetParent(combatStatViewHolder, false);
            statView.gameObject.SetActive(true);
        }

        private void ReleaseSlot(StatView statView)
        {
            statView.gameObject.SetActive(false);
            statView.transform.SetParent(transform, false);
        }

        #endregion
    }
}