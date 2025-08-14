using System;
using AdventurerVillage.HexagonGridSystem;

namespace AdventurerVillage.CraftingSystem
{
    [Serializable]
    public class CraftingInfo
    {
        #region Constructors

        public CraftingInfo()
        {
        }

        public CraftingInfo(
            string craftsmanName,
            string equipmentTypeId,
            HexCoordinates coordinates,
            int totalTickCount,
            float baseQuality,
            float totalExtraQualityRateIncrease,
            float totalProductivity)
        {
            _craftsmanName = craftsmanName;
            _equipmentTypeId = equipmentTypeId;
            _buildingCoordinates = coordinates;
            _totalTickCount = totalTickCount;
            _remainingTickCount = _totalTickCount;
            _baseQuality = baseQuality;
            _totalExtraQualityRateIncrease = totalExtraQualityRateIncrease;
            _totalProductivity = totalProductivity;
            _craftingPointPerAction = baseQuality + (baseQuality * TotalExtraQualityRateIncrease / 100f);
        }

        #endregion

        private string _craftsmanName;
        private string _equipmentTypeId;
        private HexCoordinates _buildingCoordinates;
        private int _totalTickCount;
        private int _remainingTickCount;
        private float _baseQuality;
        private float _totalExtraQualityRateIncrease;
        private float _totalProductivity;
        private float _craftingPointPerAction;
        private float _earnedCraftingPoint;
        public string CraftsmanName => _craftsmanName;
        public string EquipmentTypeId => _equipmentTypeId;
        public HexCoordinates BuildingCoordinates => _buildingCoordinates;
        public int TotalTickCount => _totalTickCount;
        public int RemainingTickCount => _remainingTickCount;
        public float BaseQuality => _baseQuality;
        public float TotalExtraQualityRateIncrease => _totalExtraQualityRateIncrease;
        public float TotalProductivity => _totalProductivity;
        public float CraftingPointPerAction => _craftingPointPerAction;
        public float EarnedCraftingPoint => _earnedCraftingPoint;

        public Action OnTimeTickChanged;
        public Action<float> OnCraftingPointAdded;

        public void OnTimeTickPassed()
        {
            _remainingTickCount--;
            OnTimeTickChanged?.Invoke();
        }

        public void IncreaseCraftingPoint(float value)
        {
            _earnedCraftingPoint += value;
            OnCraftingPointAdded?.Invoke(value);
        }
    }
}