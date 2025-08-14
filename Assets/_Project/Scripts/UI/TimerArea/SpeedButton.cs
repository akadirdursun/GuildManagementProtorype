using AdventurerVillage.TimeSystem;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.UI
{
    public class SpeedButton : MonoBehaviour
    {
        [SerializeField] private TimeSettings timeSettings;
        [SerializeField] private int speed;
        [SerializeField] private Button button;

        private void OnEnable()
        {
            button.onClick.AddListener(SetTimeSpeed);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(SetTimeSpeed);
        }

        private void SetTimeSpeed()
        {
            timeSettings.SetTimeSpeedMultiplier(speed);
        }
    }
}