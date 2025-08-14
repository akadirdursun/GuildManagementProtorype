using ADK.Common.UI;
using AdventurerVillage.TimeSystem;
using TMPro;
using UnityEngine;

namespace AdventurerVillage.UI
{
    public class HUDTimeAreaController : MonoBehaviour
    {
        [SerializeField] private DateData dateData;
        [SerializeField] private SlicedFilledImage dailyProgressFillImage;
        [SerializeField] private TMP_Text dateText;

        private void OnEnable()
        {
            DateData.OnHourPassed += OnHourPassed;
            OnHourPassed();
            DateData.OnDateChanged += OnDateChanged;
            OnDateChanged();
        }

        private void OnDisable()
        {
            DateData.OnHourPassed -= OnHourPassed;
            DateData.OnDateChanged -= OnDateChanged;
        }

        private void OnHourPassed()
        {
            dailyProgressFillImage.fillAmount = dateData.PassedPercentOfDay;
        }

        private void OnDateChanged()
        {
            dateText.text = dateData.Date;
        }
    }
}