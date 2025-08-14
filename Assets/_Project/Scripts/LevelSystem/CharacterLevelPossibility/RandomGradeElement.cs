using System;
using System.Linq;
using Random = UnityEngine.Random;

namespace AdventurerVillage.LevelSystem
{
    [Serializable]
    public struct RandomGradeElement
    {
        public Grade grade;
        public Grade maxAttributeGrade;
        public int possibility;
        public Grade Grade => grade;
        public Grade MaxAttributeGrade => maxAttributeGrade;
        public int Possibility => possibility;
    }
}