using AdventurerVillage.TooltipSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AdventurerVillage.ClassSystem.UI
{
    public class ClassIcon : MonoBehaviour, ITooltipDetector
    {
        [SerializeField] private Image iconImage;

        private BaseClass _classData;

        public void Initialize(BaseClass classData)
        {
            _classData = classData;
            iconImage.sprite = _classData.ClassIcon;
        }

        #region Tooltip

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_classData == null) return;
            TooltipSpawner.Instance.SpawnTooltipTemp(_classData.ClassName);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
        }

        #endregion
    }
}