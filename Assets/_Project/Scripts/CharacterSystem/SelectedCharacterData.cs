using System;
using AdventurerVillage.Utilities;
using UnityEngine;

namespace AdventurerVillage.CharacterSystem
{
    [CreateAssetMenu(fileName = "SelectedCharacterData",
        menuName = "Adventurer Village/Character System/Selected Character Data")]
    public class SelectedCharacterData : ScriptableObject
    {
        [SerializeField, ReadOnly]private CharacterInfo selectedCharacterInfo;
        public CharacterInfo SelectedCharacterInfo => selectedCharacterInfo;
        
        public Action OnCharacterSelected;

        public void SelectCharacter(CharacterInfo characterInfo)
        {
            selectedCharacterInfo = characterInfo;
            OnCharacterSelected?.Invoke();
        }
    }
}