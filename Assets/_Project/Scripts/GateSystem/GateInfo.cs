using System;
using AdventurerVillage.GateSystem.Interfaces;
using AdventurerVillage.HexagonGridSystem;
using AdventurerVillage.LevelSystem;
using UnityEngine;

namespace AdventurerVillage.GateSystem
{
    [Serializable]
    public class GateInfo
    {
        public GateInfo(Grade grade, HexCoordinates coordinates, IGateArea[] gateAreas)
        {
            Grade = grade;
            Coordinates = coordinates;
            GateAreas = gateAreas;
            CurrentAreaIndex = 0;
        }

        //TODO: Set Gate Name
        public string Name { get; private set; }

        //TODO: Set Gate Icon
        public Sprite Icon { get; private set; }
        public Grade Grade { get; private set; }
        public HexCoordinates Coordinates { get; private set; }
        public IGateArea[] GateAreas { get; private set; }
        public int CurrentAreaIndex { get; set; }
        public int TotalAreaCount => GateAreas.Length;
        public Vector3 WorldPosition => Coordinates.ToWorldPosition();

        public Action OnGateStateChanged { get; set; }
        public Action OnGateStateProgressChanged { get; set; }

        public string GetInfoText(bool includeGateGrade = false)
        {
            //TODO: Increase gate info
            var log = includeGateGrade ? $"Grade: {Grade}\n" : "";
            log += $"Coordinates: {Coordinates}(Dist: ??)\n";
            return log;
        }

        public override string ToString()
        {
            return $"Name: {Name}({Grade})\n" +
                   $"Coordinates: {Coordinates}\n";
        }
    }
}