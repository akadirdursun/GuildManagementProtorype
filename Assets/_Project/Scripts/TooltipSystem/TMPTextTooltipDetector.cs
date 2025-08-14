using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AdventurerVillage.TooltipSystem
{
    public class TMPTextTooltipDetector : MonoBehaviour, ITooltipDetector
    {
        [SerializeField] private TMP_Text tooltipText;

        private const int DetectionTickPerSecond = 60;
        private const float TooltipDelay = 0.25f;

        private Coroutine _detectionCoroutine;
        private int _lastSelectedLink = -1;
        private int _lastSelectedCharacter = -1;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_detectionCoroutine != null)
                StopCoroutine(_detectionCoroutine);
            _detectionCoroutine = StartCoroutine(DetectionRoutine());
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            StopCoroutine(_detectionCoroutine);
            _detectionCoroutine = null;
        }

        private bool TryToDetectLink(out int selectedLinkIndex)
        {
            selectedLinkIndex = TMP_TextUtilities.FindIntersectingLink(tooltipText, Input.mousePosition, null);
            return selectedLinkIndex != -1 && selectedLinkIndex != _lastSelectedLink;
        }

        private bool TryToDetectCharacter(out int selectedCharacterIndex)
        {
            selectedCharacterIndex =
                TMP_TextUtilities.FindIntersectingCharacter(tooltipText, Input.mousePosition, null, true);
            return selectedCharacterIndex != -1 && selectedCharacterIndex != _lastSelectedCharacter;
        }

        private void OnLinkDetected(int linkIndex)
        {
            var linkInfo = tooltipText.textInfo.linkInfo[linkIndex];
            var linkID = linkInfo.GetLinkID();
            TooltipSpawner.Instance.SpawnTooltip(linkID);
        }

        private void OnCharacterDetected(int characterIndex)
        {
            var characterInfo = tooltipText.textInfo.characterInfo[characterIndex];
            TooltipSpawner.Instance.SpawnTooltip(characterInfo.index);
        }

        private IEnumerator DetectionRoutine()
        {
            float detectionDelay = 60f / DetectionTickPerSecond;
            while (true)
            {
                if (TryToDetectLink(out var selectedLinkIndex))
                {
                    yield return new WaitForSeconds(TooltipDelay);
                    OnLinkDetected(selectedLinkIndex);
                }
                else if (TryToDetectCharacter(out var selectedCharacterIndex))
                {
                    yield return new WaitForSeconds(TooltipDelay);
                    OnCharacterDetected(selectedCharacterIndex);
                }

                yield return new WaitForSeconds(detectionDelay);
            }
        }

        private void Reset()
        {
            if (_detectionCoroutine != null)
            {
                StopCoroutine(_detectionCoroutine);
                _detectionCoroutine = null;
            }

            _lastSelectedCharacter = -1;
            _lastSelectedLink = -1;
        }
    }
}