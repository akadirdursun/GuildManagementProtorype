using System;
using System.Collections.Generic;
using System.Linq;
using CharacterInfo = AdventurerVillage.CharacterSystem.CharacterInfo;

namespace AdventurerVillage.CombatSystem
{
    [Serializable]
    public class CombatBlackboard
    {
        #region Constructors

        public CombatBlackboard(CharacterCombatConfig[] allies, CharacterCombatConfig[] enemies)
        {
            _allies = allies;
            _enemies = enemies;
        }

        #endregion

        private CharacterCombatConfig[] _allies;
        private CharacterCombatConfig[] _enemies;
        public CharacterCombatConfig[] Allies => _allies;
        public CharacterCombatConfig[] Enemies => _enemies;

        public CharacterCombatConfig[] GetEnemyCharacters(CharacterInfo requestingCharacter)
        {
            if (_enemies.Any(character => character.CharacterInfo.Name == requestingCharacter.Name))
            {
                return _allies.Where(character => character.CharacterInfo.IsAlive).ToArray();
            }

            return _enemies.Where(character => character.CharacterInfo.IsAlive).ToArray();
        }

        public CharacterCombatConfig[] GetAllyCharacters(CharacterInfo requestingCharacter)
        {
            if (_enemies.Any(character => character.CharacterInfo.Name == requestingCharacter.Name))
            {
                return _enemies.Where(character => character.CharacterInfo.IsAlive).ToArray();
            }

            return _allies.Where(character => character.CharacterInfo.IsAlive).ToArray();
        }

        public CharacterCombatConfig[] GetAllCharacters()
        {
            var characterList = new List<CharacterCombatConfig>();
            characterList.AddRange(_allies);
            characterList.AddRange(_enemies);
            return characterList.ToArray();
        }

        public bool IsCombatOver()
        {
            var isCharactersAlive = _allies.Any(character => character.CharacterInfo.IsAlive);
            var isEnemiesAlive = _enemies.Any(enemy => enemy.CharacterInfo.IsAlive);
            return !isEnemiesAlive || !isCharactersAlive;
        }
    }
}