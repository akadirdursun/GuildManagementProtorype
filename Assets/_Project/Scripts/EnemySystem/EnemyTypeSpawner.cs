using System;
using System.Collections.Generic;
using System.Linq;
using AdventurerVillage.CharacterSystem;
using AdventurerVillage.EnemySystem.Enum;
using AdventurerVillage.LevelSystem;
using AdventurerVillage.StatSystem;
using AdventurerVillage.Utilities;
using UnityEngine;
using Attribute = AdventurerVillage.StatSystem.Attribute;
using Random = UnityEngine.Random;

namespace AdventurerVillage.EnemySystem
{
    [CreateAssetMenu(fileName = "EnemyTypeSpawner", menuName = "Adventurer Village/Enemy System/Enemy Type Spawner")]
    public class EnemyTypeSpawner : ScriptableObject
    {
        [SerializeField] private GradeTable gradeTable;
        [SerializeField/*, ListDrawerSettings(ListElementLabelName = "name")*/] private EnemyTypeInfo[] enemyTypeInfos;

        //TODO: Change it. Enemy grades can be vary. Like in a D rank gate Hobgoblin with D grade can be found next to goblin ranger with E even F grade
        public EnemyInfo[] SpawnEnemy(Grade enemyGrade, EnemyCombatTypes combatType, int count)
        {
            var combatTypeEnemies = enemyTypeInfos.Where(enemy => enemy.combatType == combatType && enemyGrade >= enemy.minGrade).ToArray();
            var enemies = new EnemyInfo[count];
            gradeTable.GetLevelPoints(enemyGrade, out var minPoint, out var maxPoint);
            for (int i = 0; i < count; i++)
            {
                var randomEnemyType = combatTypeEnemies.Random();
                var randomPoint = Random.Range(minPoint, maxPoint);
                var stats = GetCharacterStats(enemyGrade, randomPoint, randomEnemyType);
                var enemyInfo = new EnemyInfo(randomEnemyType.name, randomEnemyType.combatType, enemyGrade, stats);
                enemies[i] = enemyInfo;
            }

            return enemies;
        }

        private CharacterStats GetCharacterStats(Grade enemyGrade, float attributePoint, EnemyTypeInfo enemyTypeInfo)
        {
            var maxLevel = enemyGrade == Grade.S ? Attribute.MaxLevel : gradeTable.GetRandomLevel(enemyGrade);
            var totalPoints = attributePoint * 5;
            var attributeOrder = enemyTypeInfo.attributeOrder;
            var attributeValues = new Dictionary<CharacterAttributeTypes, int>();
            int skipCountdown;
            do
            {
                skipCountdown = attributeOrder.attributeTypes.Length;
                foreach (var attributeType in attributeOrder.attributeTypes)
                {
                    var pointPerAttribute = enemyTypeInfo.attributePointData.GetAttributePointValue(attributeType);
                    if (pointPerAttribute > totalPoints)
                    {
                        attributeValues.TryAdd(attributeType, 1);
                        skipCountdown--;
                        continue;
                    }

                    var maxAttributeValue = Mathf.RoundToInt(totalPoints / pointPerAttribute);
                    if (maxAttributeValue > 1)
                    {
                        maxAttributeValue /= 2;
                    }

                    var attributeValue = Random.Range(1, maxAttributeValue + 1);
                    if (!attributeValues.TryAdd(attributeType, attributeValue))
                    {
                        var currentValue = attributeValues[attributeType];
                        currentValue += attributeValue;
                        if (currentValue > maxLevel)
                        {
                            var diff = currentValue - maxLevel;
                            currentValue = maxLevel;
                            attributeValue -= diff;
                            skipCountdown--;
                        }

                        attributeValues[attributeType] = currentValue;
                    }

                    totalPoints -= attributeValue * pointPerAttribute;
                }
            } while (skipCountdown > 0);

            return new CharacterStats(
                CreateAttribute(CharacterAttributeTypes.Strength),
                CreateAttribute(CharacterAttributeTypes.Constitution),
                CreateAttribute(CharacterAttributeTypes.Agility),
                CreateAttribute(CharacterAttributeTypes.Intelligence),
                CreateAttribute(CharacterAttributeTypes.Magic));

            Attribute CreateAttribute(CharacterAttributeTypes attributeType)
            {
                var currentLevel = attributeValues[attributeType];
                return new Attribute(currentLevel, gradeTable.GetGradeFromLevel);
            }
        }

        #region structs

        [Serializable]
        public struct EnemyTypeInfo
        {
            public string name;
            public Grade minGrade;
            public EnemyCombatTypes combatType;
            public CharacterAttributePointData attributePointData;
            public AttributeOrder attributeOrder;
        }

        #endregion
    }
}