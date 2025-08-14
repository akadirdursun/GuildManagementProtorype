using System;
using PrimeTween;
using TMPro;
using UnityEngine;

namespace AdventurerVillage.BuildingSystem.UI
{
    public class CraftPointView : MonoBehaviour
    {
        [SerializeField] private TMP_Text valueText;
        [SerializeField] private float animationDuration = 1f;
        [SerializeField] private float moveDistance = 10f;
        public Action OnCompleteAction;

        public void Initialize(float addedValue)
        {
            valueText.text = $"+{addedValue}";
            PlayAnimation();
        }

        private void PlayAnimation()
        {
            Tween.LocalPositionY(transform, moveDistance, animationDuration, ease: Ease.Linear).OnComplete(() =>
            {
                transform.localPosition = Vector3.zero;
                OnCompleteAction?.Invoke();
            });
        }
    }
}