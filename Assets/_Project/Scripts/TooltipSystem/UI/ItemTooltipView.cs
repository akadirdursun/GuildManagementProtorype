using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.TooltipSystem.UI
{
    public class ItemTooltipView : BaseTooltipView
    {
        [SerializeField] protected Image iconImage;
        [SerializeField] protected TMP_Text infoText;

        public void Initialize(Sprite itemIcon, string title, string effectInfo, string description)
        {
            iconImage.sprite = itemIcon;
            titleText.text = title;
            infoText.text = effectInfo;
            descriptionText.text = description;
        }

        public override void Unlock()
        {
            base.Unlock();
            if (_isFocused) return;
            TooltipSpawner.Instance.ReleaseTooltip(this);
        }

        protected override void Reset()
        {
            base.Reset();
            iconImage.sprite = null;
            infoText.text = "";
        }

        protected override void OnTooltipLostFocus()
        {
            TooltipSpawner.Instance.ReleaseTooltip(this);
        }
    }
}