using System.Linq;
using AdventurerVillage.CharacterSystem;
using AdventurerVillage.HexagonGridSystem;
using AdventurerVillage.LevelSystem;
using AdventurerVillage.Utilities;
using UnityEngine;

namespace AdventurerVillage.GateSystem
{
    //TODO: Refactor
    public class GateSpawner : MonoBehaviour
    {
        [SerializeField] private GateDatabase gateDatabase;
        [SerializeField] private GateSpawnerConfig gateSpawnerConfig;
        [SerializeField] private HexMapData hexMapData;
        [SerializeField] private CharacterDatabase characterDatabase;

        [Tooltip(
            "Scaling factor to prioritize higher grades. Decrease scalingFactor if lower-tier dungeons dominate spawns")]
        [SerializeField]
        private float scalingFactor = 1.5f;

        [Tooltip(
            "Boosts spawn chance of higher grades (even with few characters). Increase if higher grade dungeons feel too rare")]
        [SerializeField]
        private float minBaseWeight = 0.3f;

        [ContextMenu("Spawn Gates")]
        public void SpawnGate()
        {
            var gradeGroups = characterDatabase.CountCharacters();
            var gateGrade = GateGrader.SelectGateGrade(gradeGroups, scalingFactor, minBaseWeight);
            var possibleGateCells =
                hexMapData.GetCoordinatesInDistanceToCity(gateSpawnerConfig.MinDistanceToCity,
                    gateSpawnerConfig.MaxDistanceToCity);
            //TODO: Remove already spawned gate positions from the list
            possibleGateCells.Shuffle();
            var gateCell = possibleGateCells.First();
            var gateInfo = CreateGateInfo(gateGrade, gateCell.Coordinates);
            gateDatabase.AddGateInfo(gateInfo);
            //TODO: gateCell.SpawnGate(gateInfo);
        }

        private GateInfo CreateGateInfo(Grade gateGrade, HexCoordinates gateCoordinates)
        {
            var data = gateSpawnerConfig.GateData.First(d => d.Grade == gateGrade);
            return data.CreateGateInfo(gateCoordinates);
        }

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            GatePointData.SpawnNewGate += SpawnGate;
        }

        private void Start()
        {
            var gates = gateDatabase.GateInfos;
            foreach (var gateInfo in gates)
            {
                hexMapData.TryGetCell(gateInfo.Coordinates, out var cell);
                //TODO: cell.SpawnGate(gateInfo);
            }
        }

        private void OnDisable()
        {
            GatePointData.SpawnNewGate -= SpawnGate;
        }

        #endregion
    }
}