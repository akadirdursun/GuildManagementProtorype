using System;
using AdventurerVillage.Utilities;
using UnityEngine;

namespace AdventurerVillage.ResourceSystem
{
    [CreateAssetMenu(fileName = "NewResourceData", menuName = "Adventurer Village/Resource System/Resource Data")]
    public class ResourceData : ScriptableObject
    {
        [SerializeField, ReadOnly] private string id;
        [SerializeField, Space] private DefaultResourceAmounts defaultResourceAmounts;
        [SerializeField] private string resourceName;
        [SerializeField] private Sprite resourceIcon;
        [SerializeField, ReadOnly] private float amount;
        [SerializeField, ReadOnly] private float gainPerHour;

        public Action OnResourceAmountChanged;

        public string ID => id;
        public string ResourceName => resourceName;
        public Sprite ResourceIcon => resourceIcon;
        public float Amount => amount;
        public float GainPerHour => gainPerHour;

        public void Initialize()
        {
            amount = defaultResourceAmounts.resourceAmount;
            gainPerHour = defaultResourceAmounts.gainPerHour;
            OnResourceAmountChanged?.Invoke();
        }

        public void Initialize(float resourceAmount, float resourceGainPerHour)
        {
            amount = resourceAmount;
            gainPerHour = resourceGainPerHour;
            OnResourceAmountChanged?.Invoke();
        }

        public void AddAmount(float addAmount)
        {
            amount += addAmount;
            OnResourceAmountChanged?.Invoke();
        }

        public bool TryToSpendAmount(float spendAmount)
        {
            if (amount < spendAmount) return false;

            amount -= spendAmount;
            OnResourceAmountChanged?.Invoke();
            return true;
        }

        public void ChangeGainPerHour(float increaseAmount)
        {
            gainPerHour += increaseAmount;
        }

        public bool HasAmount(float spendAmount)
        {
            return amount >= spendAmount;
        }

        public void OnAHourPassed()
        {
            if (gainPerHour == 0) return;
            AddAmount(gainPerHour);
        }

        #region ScriptableObject Methods

        private void Awake()
        {
            if (!string.IsNullOrEmpty(id)) return;
            id = Guid.NewGuid().ToString();
        }

        #endregion

        #region Structs

        [Serializable]
        private struct DefaultResourceAmounts
        {
            public float resourceAmount;
            public float gainPerHour;
        }

        #endregion
    }
}