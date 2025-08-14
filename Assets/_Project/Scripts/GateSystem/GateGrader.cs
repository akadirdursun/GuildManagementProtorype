using System.Collections.Generic;
using AdventurerVillage.LevelSystem;
using UnityEngine;

namespace AdventurerVillage.GateSystem
{
    public static class GateGrader 
    {
        private static readonly List<Grade> GradeValues = new()
        {
            Grade.F,
            Grade.E,
            Grade.D,
            Grade.C,
            Grade.B,
            Grade.A,
            Grade.S
        };
        
        public static Grade SelectGateGrade(Dictionary<Grade, int> gradeGroups, float scalingFactor, float minBaseWeight)
        {
            List<Grade> eligibleGrades = new List<Grade>();
            foreach (var grade in GradeValues)
            {
                if (CanClearGrade(grade))
                    eligibleGrades.Add(grade);
            }

            if (eligibleGrades.Count == 0)
            {
                Debug.LogError("No eligible grades found");
                return Grade.F;
            }
            Dictionary<Grade, float> weights = new Dictionary<Grade, float>();
            float totalWeight = 0f;

            foreach (var grade in eligibleGrades)
            {
                int eligibleCount = GetEligibleCharacterCount(grade);
                int gradeVal = (int)grade + 1; //+1 because grades starts from 0 and ends with 6
                // Calculate base weight and minimum weight
                float baseWeight = eligibleCount * Mathf.Pow(scalingFactor, gradeVal);
                float minWeight = minBaseWeight * gradeVal; // Grade scales the minimum
                float totalGradeWeight = baseWeight + minWeight;
                weights[grade] = totalGradeWeight;
                totalWeight += totalGradeWeight;
            }

            float random = Random.Range(0, totalWeight);
            float current = 0f;

            foreach (var grade in eligibleGrades)
            {
                current += weights[grade];
                if (random <= current)
                {
                    Debug.Log($"<color=green>Eligible Grade: {grade}</color>");
                    return grade;
                }
            }

            return eligibleGrades[^1];
            
            bool CanClearGrade(Grade targetGrade)
            {
                //TODO: Later set this to the date. After date x higher grade dungeons can be spawn 
                int targetVal = (int)targetGrade - 1;
                foreach (var grade in gradeGroups.Keys)
                {
                    if ((int)grade >= targetVal && gradeGroups[grade] > 0)
                        return true;
                }

                return false;
            }
            
            int GetEligibleCharacterCount(Grade targetGrade)
            {
                int targetVal = (int)targetGrade;
                int count = 0;
                foreach (var grade in gradeGroups.Keys)
                {
                    if ((int)grade >= targetVal)
                        count += gradeGroups[grade];
                }

                return count;
            }
        }
    }
}