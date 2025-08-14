using System.Linq;
using UnityEngine;

namespace AdventurerVillage.LevelSystem
{
    [CreateAssetMenu(fileName = "LevelPossibilityTable",
        menuName = "Adventurer Village/Level System/Grade Possibility Table")]
    public class GradePossibilityTable : ScriptableObject
    {
        [SerializeField/*, ListDrawerSettings(ListElementLabelName = "grade")*/] private RandomGradeElement[] randomGradeElements;
        
        
        public void GetRandomGrade(out Grade grade, out Grade maxAttributeGrade)
        {
            var levelElement = GetRandomGrade();
            grade = levelElement.Grade;
            maxAttributeGrade = levelElement.maxAttributeGrade;
        }

        private RandomGradeElement GetRandomGrade()
        {
            var randomPossibility = Random.Range(1f, GetTotalPossibility());
            var characterLevelElementPossibility = 0f;
            var selectedCharacterLevelElement = randomGradeElements.Last(e =>
            {
                characterLevelElementPossibility += e.Possibility;
                return randomPossibility <= characterLevelElementPossibility;
            });

            return selectedCharacterLevelElement;
        }

        public override string ToString()
        {
            var totalPossibility = GetTotalPossibility();
            var percentageDiff = 100f / totalPossibility;
            var value = "";

            foreach (var element in randomGradeElements)
            {
                var percentage = element.possibility * percentageDiff;
                value += $"• {percentage:N1}% of Grade {element.Grade} chance\n";
            }

            return value;
        }

        private float GetTotalPossibility()
        {
            var totalPossibility = 0f;
            foreach (var characterLevelElement in randomGradeElements)
            {
                totalPossibility += characterLevelElement.Possibility;
            }

            return totalPossibility;
        }
    }
}