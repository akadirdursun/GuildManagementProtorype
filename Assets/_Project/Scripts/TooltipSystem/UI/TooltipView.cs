namespace AdventurerVillage.TooltipSystem.UI
{
    public class TooltipView : BaseTooltipView
    {
        public void Initialize(string title, string description)
        {
            titleText.text = title;
            descriptionText.text = description;
        }

        public override void Unlock()
        {
            base.Unlock();
            if (_isFocused) return;
            TooltipSpawner.Instance.ReleaseTooltip(this);
        }

        protected override void OnTooltipLostFocus()
        {
            TooltipSpawner.Instance.ReleaseTooltip(this);
        }
    }
}