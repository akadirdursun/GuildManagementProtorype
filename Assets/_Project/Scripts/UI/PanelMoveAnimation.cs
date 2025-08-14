using PrimeTween;
using UnityEngine;

namespace AdventurerVillage.UI
{
    public class PanelMoveAnimation : MonoBehaviour
    {
        [SerializeField] private RectTransform target;
        [SerializeField] private bool vertical;
        [SerializeField] private bool moveFromPositive;
        [SerializeField] private float animationTime = 1f;
        [SerializeField] private Ease animationEase = Ease.Linear;

        [ContextMenu("PlayAnimation")]
        public void PlayMoveAnimation(bool hideFirst = false)
        {
            if (hideFirst)
                HidePanel();

            Tween.LocalPosition(target, Vector3.zero, animationTime, animationEase);
        }

        [ContextMenu("HidePanel")]
        public void HidePanel()
        {
            var distance = vertical ? target.rect.height : target.rect.width;
            var direction = vertical ? Vector3.up : Vector3.right;
            target.localPosition = moveFromPositive ? direction * distance : -direction * distance;
        }
    }
}