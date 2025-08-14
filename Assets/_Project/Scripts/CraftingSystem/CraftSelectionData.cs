using System;
using AdventurerVillage.BuildingSystem;
using AdventurerVillage.ItemSystem.Equipments;
using AdventurerVillage.ResourceSystem;
using UnityEngine;
using CharacterInfo = AdventurerVillage.CharacterSystem.CharacterInfo;

namespace AdventurerVillage.CraftingSystem
{
    [CreateAssetMenu(fileName = "CraftSelectionData", menuName = "Adventurer Village/Crafting System/Craft Selection Data")]
    public class CraftSelectionData : ScriptableObject
    {
        [SerializeField] private EquipmentCraftConfig equipmentCraftConfig;
        [SerializeField] private WorkshopBuildingData equipmentWorkshopBuildData;
        [SerializeField] private ResourceData materialResources;
        private BuildingInfo _selectedWorkshop;
        private EquipmentData _selectedEquipmentData;
        private CharacterInfo _selectedCraftsman;

        public Action OnWorkshopSelected;
        public Action OnEquipmentTypeSelected;
        public Action OnCraftsmanSelected;
        public Action OnStaminaCostChanged;
        public Action OnExtraMaterialCostChanged;
        public Action OnCraftSelectionCleared;

        public BuildingInfo SelectedWorkshop => _selectedWorkshop;
        public EquipmentData SelectedEquipmentData => _selectedEquipmentData;
        public CharacterInfo SelectedCraftsman => _selectedCraftsman;
        public int CurrentStaminaCost { get; private set; }
        public int BaseStaminaCost => _selectedEquipmentData == null ? 0 : _selectedEquipmentData.BaseStaminaForCraft;
        public int ExtraMaterialCount { get; private set; }
        public int TotalMaterialCost => _selectedEquipmentData.CraftCost + ExtraMaterialCount;
        public int CraftTimeTickCount { get; private set; }
        public bool IsCraftSelectionOngoing { get; private set; }

        public CraftingInfo CreateCraftingInfo()
        {
            materialResources.TryToSpendAmount(TotalMaterialCost);
            _selectedCraftsman.Stats.Stamina.Reduce(CurrentStaminaCost);
            var workshopQualityRate = equipmentWorkshopBuildData.GetCraftBuildingLevelInfo(_selectedWorkshop.BuildingLevel)
                .workShopQualityRate;
            var totalExtraQualityRate = workshopQualityRate + _selectedCraftsman.Stats.CraftQuality.Value;
            var extraProductivityFromMaterial = ProductivityFromExtraMaterial();
            var totalProductivity = extraProductivityFromMaterial + _selectedCraftsman.Stats.CraftProductivity.Value;
            return new CraftingInfo(
                _selectedCraftsman.Name,
                _selectedEquipmentData.Id,
                _selectedWorkshop.Coordinates,
                CraftTimeTickCount,
                GameConfig.BaseCraftingQualityValue,
                totalExtraQualityRate,
                totalProductivity
            );
        }

        public void SelectWorkshop(BuildingInfo selectedWorkshop)
        {
            _selectedWorkshop = selectedWorkshop;
            OnWorkshopSelected?.Invoke();
            IsCraftSelectionOngoing = true;
        }

        public void SelectEquipment(EquipmentData selectedEquipmentData)
        {
            _selectedEquipmentData = selectedEquipmentData;
            ExtraMaterialCount = _selectedEquipmentData.CraftCost;
            CurrentStaminaCost = _selectedEquipmentData.BaseStaminaForCraft;
            CraftTimeTickCount = CurrentStaminaCost * equipmentCraftConfig.TickPerStamina;
            OnEquipmentTypeSelected?.Invoke();
        }

        public void SelectCraftsman(CharacterInfo selectedCraftsman)
        {
            _selectedCraftsman = selectedCraftsman;
            OnCraftsmanSelected?.Invoke();
        }

        public void ChangeStaminaCost(int staminaCost)
        {
            CurrentStaminaCost = staminaCost;
            CraftTimeTickCount = CurrentStaminaCost * equipmentCraftConfig.TickPerStamina;
            OnStaminaCostChanged?.Invoke();
        }

        public void ChangeExtraMaterialCount(int extraMaterialCount)
        {
            ExtraMaterialCount = extraMaterialCount;
            OnExtraMaterialCostChanged?.Invoke();
        }

        public float ProductivityFromExtraMaterial()
        {
            var maxExtraMaterialCount =
                _selectedEquipmentData.CraftCost * (equipmentCraftConfig.MaxExtraMaterialMultiplier - 1);
            var productivityFromExtraMaterial = (ExtraMaterialCount / maxExtraMaterialCount) *
                                                equipmentCraftConfig.MaxProductivityFromExtraMaterial;
            return productivityFromExtraMaterial;
        }

        public bool IsCraftPossible()
        {
            var hasEnoughMaterial = materialResources.HasAmount(TotalMaterialCost);
            var hasEnoughStamina = _selectedCraftsman.Stats.Stamina.HasEnoughValue(CurrentStaminaCost);
            return hasEnoughStamina && hasEnoughMaterial;
        }

        public void Clear()
        {
            _selectedWorkshop = null;
            _selectedEquipmentData = null;
            _selectedCraftsman = null;
            CurrentStaminaCost = 0;
            CraftTimeTickCount = CurrentStaminaCost * equipmentCraftConfig.TickPerStamina;
            IsCraftSelectionOngoing = false;
            OnCraftSelectionCleared?.Invoke();
        }
    }
}