using System.Collections.Generic;
using AdventurerVillage.CharacterSystem;
using AdventurerVillage.CharacterSystem.CharacterCustomization;
using AdventurerVillage.LevelSystem;
using AdventurerVillage.NameGenerationSystem;
using AdventurerVillage.StatSystem;
using AdventurerVillage.TraitSystem;
using AdventurerVillage.UI;
using AdventurerVillage.Utilities;
using UnityEngine;
using Attribute = AdventurerVillage.StatSystem.Attribute;
using CharacterInfo = AdventurerVillage.CharacterSystem.CharacterInfo;
using Random = UnityEngine.Random;

namespace AdventurerVillage.AwakenSystems
{
    [CreateAssetMenu(fileName = "CharacterAwakenController", menuName = "Adventurer Village/Awaken System/Character Awaken Controller")]
    public class CharacterAwakenController : ScriptableObject
    {
        [SerializeField] private GradeTable gradeTable;
        [SerializeField] private CharacterDatabase characterDatabase;
        [SerializeField] private CharacterModelRandomizer characterModelRandomizer;
        [SerializeField] private CharacterNameGenerator characterNameGenerator;
        [SerializeField] private CharacterAttributePointData characterAttributePointData;
        [SerializeField] private AttributeOrder[] attributeOrders;

        private const float CharacterPointMultiplier = 5f;

        public CharacterInfo AwakenCharacter(Grade grade, Grade maxGrade)
        {
            var randomCharacterModel = characterModelRandomizer.RandomizeCharacter();
            return AwakenCharacter(grade, maxGrade, randomCharacterModel);
        }

        public CharacterInfo AwakenCharacter(Grade grade, Grade maxGrade, CharacterModelInfo characterModel)
        {
            var characterName = characterNameGenerator.GetName(characterModel.gender);
            var stats = CreateCharacterStats(grade, maxGrade);
            var traits = GetTraits();
            var characterInfo = new CharacterInfo(
                characterName,
                stats,
                traits,
                characterModel);
            UI3DCharacterController.Instance.ClipTexture(characterInfo);
            characterDatabase.AddCharacter(characterInfo);
            return characterInfo;
        }

        private CharacterStats CreateCharacterStats(Grade grade, Grade maxGrade)
        {
            gradeTable.GetLevelPoints(grade, out var minPoint, out var maxPoint);
            var maxLevel = maxGrade == Grade.S ? Attribute.MaxLevel : gradeTable.GetRandomLevel(maxGrade);
            var randomPoint = Random.Range(minPoint, maxPoint);
            var totalPoint = randomPoint * CharacterPointMultiplier;
            var randomOrder = attributeOrders.Random();
            var attributeValues = new Dictionary<CharacterAttributeTypes, int>();
            int skipCountdown;
            do
            {
                skipCountdown = randomOrder.attributeTypes.Length;
                foreach (var attributeType in randomOrder.attributeTypes)
                {
                    var pointPerAttribute = characterAttributePointData.GetAttributePointValue(attributeType);
                    if (pointPerAttribute > totalPoint)
                    {
                        attributeValues.TryAdd(attributeType, 1);
                        skipCountdown--;
                        continue;
                    }

                    var maxAttributeValue = Mathf.RoundToInt(totalPoint / pointPerAttribute);
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

                    totalPoint -= attributeValue * pointPerAttribute;
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

        private List<AcquiredTraitInfo> GetTraits()
        {
            return new List<AcquiredTraitInfo>();
        }
    }
}