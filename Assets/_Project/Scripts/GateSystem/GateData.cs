using System;
using System.Collections.Generic;
using AdventurerVillage.GateSystem.Interfaces;
using AdventurerVillage.HexagonGridSystem;
using AdventurerVillage.LevelSystem;
using AdventurerVillage.Utilities;
using UnityEngine;
using CharacterInfo = AdventurerVillage.CharacterSystem.CharacterInfo;
using Random = UnityEngine.Random;

namespace AdventurerVillage.GateSystem
{
    [Serializable]
    public struct GateData
    {
        [SerializeField] private Grade grade;
        [SerializeField] private int minGateAreaAmount;
        [SerializeField] private int maxGateAreaAmount;
        [SerializeField] private int maxPartySize;
        [SerializeField, Range(0f, 1f)] private float combatRoomPercentage;

        [SerializeField/*, TabGroup("Combat Settings")*/] private int minEnemyAmount;
        [SerializeField/*, TabGroup("Combat Settings")*/] private int maxEnemyAmount;
        [SerializeField/*, TabGroup("Combat Settings")*/, Range(0.01f, 1f)] private float baseMultiplier;
        [SerializeField/*, TabGroup("Resource Settings")*/] private GateResourceInfo[] resourceInfos;

        public Grade Grade => grade;

        public GateInfo CreateGateInfo(HexCoordinates gateCoordinates)
        {
            var gateAreaList = new List<IGateArea>();
            var gateAreaCount = Random.Range(minGateAreaAmount, maxGateAreaAmount + 1);
            var combatAreaCount = Mathf.RoundToInt(gateAreaCount * combatRoomPercentage);
            var resourceAreaCount = gateAreaCount - combatAreaCount;
            var combatAreas = CreateCombatAreas(combatAreaCount);
            gateAreaList.AddRange(combatAreas);
            var resourceAreas = CreateResourceAreas(resourceAreaCount);
            gateAreaList.AddRange(resourceAreas);
            gateAreaList.Shuffle();
            return new GateInfo(grade, gateCoordinates, gateAreaList.ToArray());
        }

        private GateResourceArea[] CreateResourceAreas(int resourceAreaCount)
        {
            var resourceAreas = new GateResourceArea[resourceAreaCount];
            for (int i = 0; i < resourceAreaCount; i++)
            {
                var resourceInfo = resourceInfos.Random();
                resourceAreas[i] = new GateResourceArea(resourceInfo.ResourceData.ID, resourceInfo.Amount);
            }

            return resourceAreas;
        }

        private GateCombatArea[] CreateCombatAreas(int combatAreaCount)
        {
            var combatAreas = new GateCombatArea[combatAreaCount];
            var enemies = CreateEnemies();
            var enemyCount = enemies.Count;
            var enemyPerArea = enemyCount / combatAreaCount;
            var diffOnEnemyCount = enemyCount % combatAreaCount;
            for (int i = 0; i < combatAreaCount; i++)
            {
                var targetEnemyCount = enemyPerArea;
                if (diffOnEnemyCount > 0)
                {
                    targetEnemyCount++;
                    diffOnEnemyCount--;
                }

                if (enemies.Count < targetEnemyCount)
                {
                    targetEnemyCount = enemies.Count;
                }

                var areaEnemies = enemies.GetRange(0, targetEnemyCount).ToArray();
                enemies.RemoveRange(0, targetEnemyCount);
                combatAreas[i] = new GateCombatArea(areaEnemies);
            }

            return combatAreas;
        }

        private List<CharacterInfo> CreateEnemies()
        {
            var totalEnemyCount = Random.Range(minEnemyAmount, maxEnemyAmount + 1);
            var enemyGrades = new Dictionary<Grade, int>();
            var weights = new Dictionary<Grade, float>();
            var totalWeight = 0f;
            for (int i = (int)grade; i >= 0; i--)
            {
                var weight = Mathf.Pow(baseMultiplier, i);
                weights.Add((Grade)i, weight);
                totalWeight += weight;
            }

            int calculatedEnemyCount = 0;
            foreach (var weight in weights)
            {
                var multiplier = weight.Value / totalWeight;
                int enemyCount = Mathf.RoundToInt(totalEnemyCount * multiplier);
                calculatedEnemyCount += enemyCount;
                enemyGrades.Add(weight.Key, enemyCount);
            }

            if (calculatedEnemyCount < totalEnemyCount)
            {
                var diff = totalEnemyCount - calculatedEnemyCount;
                enemyGrades[Grade.F] += diff;
                Debug.LogError($"There is a {diff} amount of diff from calculated enemy count");
            }

            var enemies = new List<CharacterInfo>(totalEnemyCount);
            /*var enemyCreator = enemyCreators.Random();
            foreach (var grades in enemyGrades)
            {
                for (int i = 0; i < grades.Value; i++)
                {
                    enemies.Add(enemyCreator.CreateCharacter(grades.Key));
                }
            }*/

            enemies.Shuffle();
            return enemies;
        }
    }
}