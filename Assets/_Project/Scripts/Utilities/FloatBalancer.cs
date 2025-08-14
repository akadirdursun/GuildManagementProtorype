using UnityEngine;

namespace AdventurerVillage.Utilities
{
    public static class FloatBalancer
    {
        public static void BalanceToOne(ref float first, ref float second, float changedValue)
        {
            var diff = 1f - (changedValue);
            diff -= (first + second);
            var adjustment = diff / 2;
            if (adjustment < 0 && (first == 0 || second == 0))
                adjustment *= 2;
            first = Mathf.Clamp01(first + adjustment);
            second = Mathf.Clamp01(second + adjustment);
        }
    }
}