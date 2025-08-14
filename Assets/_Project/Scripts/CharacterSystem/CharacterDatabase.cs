using System.Collections.Generic;
using System.Linq;
using AdventurerVillage.LevelSystem;
using AdventurerVillage.SaveSystem;
using UnityEngine;

namespace AdventurerVillage.CharacterSystem
{
    [CreateAssetMenu(fileName = "CharacterDatabase",
        menuName = "Adventurer Village/Character System/Character Database")]
    public class CharacterDatabase : SavableScriptableObject
    {
        [SerializeField] private GradeTable gradeTable;

        [SerializeField]
        private List<CharacterInfo> characterInfoList;

        private HashSet<string> _characterNames = new();
        public CharacterInfo[] AllCharacters => characterInfoList.ToArray();
        public CharacterInfo[] PartyFreeCharacters => characterInfoList.Where(c => !c.IsPartyMember).ToArray();

        public void AddCharacter(CharacterInfo characterInfo)
        {
            characterInfoList.Add(characterInfo);
            _characterNames.Add(characterInfo.Name);
        }

        public bool IsCharacterNameExists(string characterName)
        {
            return _characterNames.Contains(characterName);
        }

        public Dictionary<Grade, int> CountCharacters()
        {
            var gradeGroups = new Dictionary<Grade, int>();
            var groups = characterInfoList.GroupBy(c => gradeTable.GetGradeFromLevel(c.AverageLevel))
                .ToDictionary(g => g.Key, g => g.ToList());
            foreach (var group in groups)
            {
                gradeGroups.Add(group.Key, group.Value.Count);
            }

            return gradeGroups;
        }

        #region Save System

        public override void Save()
        {
            //ES3.Save("characterInfoList", characterInfoList);
            //ES3.Save("_characterNames", _characterNames);
        }

        public override void Load()
        {
            //characterInfoList = ES3.Load("characterInfoList", new List<CharacterInfo>());
            //_characterNames = ES3.Load("_characterNames", new HashSet<string>());
        }

        public override void Reset()
        {
            characterInfoList = new();
            _characterNames = new();
        }

        #endregion
    }
}