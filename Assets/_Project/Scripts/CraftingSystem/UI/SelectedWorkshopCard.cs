using System;
using UnityEngine;

namespace AdventurerVillage.CraftingSystem.UI
{
    public class SelectedWorkshopCard : WorkshopSelectionCard
    {
        [SerializeField] private CanvasGroup canvasGroup;
        private Action _clickAction;

        public void Initialize(Action clickAction)
        {
            _clickAction = clickAction;
        }

        public void Show()
        {
            Initialize(craftSelectionData.SelectedWorkshop);
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
        }

        public void Hide()
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0f;
        }

        protected override void OnSelectButtonClick()
        {
            base.OnSelectButtonClick();
            _clickAction?.Invoke();
        }
    }
}