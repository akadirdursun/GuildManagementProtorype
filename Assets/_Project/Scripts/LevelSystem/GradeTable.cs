using System;
using System.Linq;
using AdventurerVillage.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AdventurerVillage.LevelSystem
{
    [CreateAssetMenu(fileName = "GradeTable", menuName = "Adventurer Village/Level System/Grade Table")]
    public class GradeTable : ScriptableObject
    {
        [SerializeField/*, ListDrawerSettings(ListElementLabelName = "grade")*/] private GradeTableElement[] gradeTableElements;

        private const float MaxLevelPoint = 1000f;

        public Grade GetGradeFromLevel(float level)
        {
            return gradeTableElements.Last(table => table.minLevel <= level).grade;
        }

        public Grade GetGradeFromPoint(float point)
        {
            return gradeTableElements.Last(table => table.minPoint <= point).grade;
        }

        public Color GetColor(Grade grade)
        {
            return gradeTableElements.First(l => l.grade == grade).color;
        }

        public int GetRandomLevel(Grade grade, int minLevel = 1)
        {
            var tableIndex = gradeTableElements.FindIndex(table => table.grade == grade);
            var minValue = gradeTableElements[tableIndex].minLevel;
            var maxValue = tableIndex == gradeTableElements.Length - 1 ? 100 : gradeTableElements[tableIndex + 1].minLevel - 1;
            minValue = minValue < minLevel ? minLevel : minValue;
            return Random.Range(minValue, maxValue + 1);
        }

        public float GetMinLevel(Grade grade)
        {
            var tableIndex = gradeTableElements.FindIndex(table => table.grade == grade);
            return tableIndex == 0 ? 1f : gradeTableElements[tableIndex - 1].minLevel;
        }

        public void GetLevelPoints(Grade grade, out float minPoint, out float maxPoint)
        {
            var elementIndex = gradeTableElements.FindIndex(element => element.grade == grade);
            minPoint = gradeTableElements[elementIndex].minPoint;
            maxPoint = elementIndex < gradeTableElements.Length - 1 ? gradeTableElements[elementIndex + 1].minPoint - 0.1f : MaxLevelPoint;
        }

        public float[] GetPointGradeArray()
        {
            return Array.ConvertAll(gradeTableElements, element => element.minPoint);
        }
    }
}