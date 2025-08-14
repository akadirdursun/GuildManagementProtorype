using PrimeTween;
using TMPro;
using UnityEngine;

namespace AdventurerVillage.UI
{
    public class WarningTextAnimation : MonoBehaviour
    {
        [SerializeField] private TMP_Text warningText;
        [SerializeField] private float moveDistance = 300f;
        [SerializeField] private float duration = 1f;

        private Sequence _sequence;

        public void PlayAnimation()
        {
            if (_sequence.isAlive) return;
            warningText.transform.localPosition = Vector3.zero;
            warningText.gameObject.SetActive(true);
            _sequence = Sequence.Create();
            _sequence.Chain(Tween.LocalPositionY(warningText.transform, moveDistance, duration));
            _sequence.Group(Tween.Alpha(warningText, 0f, .5f, startDelay: .5f));
            _sequence.OnComplete(() =>
            {
                warningText.gameObject.SetActive(false);
                warningText.alpha = 1f;
            });
        }
    }
}