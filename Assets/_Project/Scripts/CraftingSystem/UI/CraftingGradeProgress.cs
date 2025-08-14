using System;
using ADK.Common.UI;
using AdventurerVillage.LevelSystem;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.CraftingSystem.UI
{
    public class CraftingGradeProgress : MonoBehaviour
    {
        [SerializeField] private CraftingPointData craftingPointData;
        [SerializeField] private GradeTable gradeTable;
        [SerializeField] private TMP_Text gradeText;
        [SerializeField] private Image frameImage;
        [SerializeField] private SlicedFilledImage fillImage;
        [SerializeField] private float timePerFill = .5f;
        [SerializeField] private float textScaleAnimationTime = .5f;
        [SerializeField] private float textAnimationMaxScale = 4f;

        public float Initialize(float craftingPoints, Action onComplete)
        {
            Clear();
            var pointArray = gradeTable.GetPointGradeArray();
            //Calculate animation duration
            var targetGradeIndex = (int)gradeTable.GetGradeFromPoint(craftingPoints);
            var targetGradeMaxValue = pointArray[Mathf.Clamp(targetGradeIndex + 1, 0, pointArray.Length - 1)];
            var animationDuration = timePerFill * (targetGradeIndex + craftingPoints / targetGradeMaxValue);
            //
            int pointIndex = 1;
            float startValue = 0;
            float tarGetValue = pointArray[pointIndex];
            Tween.Custom(0, craftingPoints, animationDuration, OnValueChanged, endDelay: .5f).OnComplete(OnComplete);
            return animationDuration;

            void OnValueChanged(float value)
            {
                value -= startValue;
                if (value > tarGetValue)
                {
                    var grade = (Grade)pointIndex;
                    pointIndex++;
                    startValue = tarGetValue;
                    tarGetValue = pointArray[pointIndex] - startValue;
                    var gradeColor = gradeTable.GetColor(grade);
                    gradeText.color = gradeColor;
                    fillImage.color = gradeColor;
                    ChangeGradeText($"{grade}");
                }

                fillImage.fillAmount = value / tarGetValue;
            }

            void OnComplete()
            {
                onComplete?.Invoke();
            }
        }

        private void ChangeGradeText(string newText)
        {
            gradeText.transform.localScale = Vector3.one * textAnimationMaxScale;
            gradeText.text = newText;
            Tween.Scale(gradeText.transform, 1f, textScaleAnimationTime);
        }

        private void Clear()
        {
            var fGradeColor = gradeTable.GetColor(Grade.F);
            gradeText.color = fGradeColor;
            gradeText.text = "F";
            fillImage.color = fGradeColor;
            fillImage.fillAmount = 0f;
        }
    }
}