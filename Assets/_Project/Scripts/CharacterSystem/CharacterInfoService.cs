using System.Linq;
using ADK.Common;
using UnityEngine;

namespace AdventurerVillage.CharacterSystem
{
    public class CharacterInfoService : Singleton<CharacterInfoService>
    {
        [SerializeField] private CharacterDatabase characterDatabase;

        //TODO: Just searching a huge array is waste of time. Refactor this
        public bool TryToGetCharacterInfo(string characterName, out CharacterInfo characterInfo)
        {
            characterInfo = characterDatabase.AllCharacters.FirstOrDefault(c => c.Name == characterName);
            return characterName != null;
        }
    }
}