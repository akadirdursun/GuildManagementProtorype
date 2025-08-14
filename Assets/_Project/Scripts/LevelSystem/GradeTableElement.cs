using System;
using UnityEngine;

namespace AdventurerVillage.LevelSystem
{
    [Serializable]
    public struct GradeTableElement
    {
        public Grade grade;
        public Color color;
        public int minLevel;
        public float minPoint;
    }
}