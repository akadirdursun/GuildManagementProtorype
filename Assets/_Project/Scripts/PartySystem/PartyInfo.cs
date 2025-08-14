using System;
using System.Collections.Generic;
using AdventurerVillage.CharacterSystem;
using AdventurerVillage.InventorySystem;
using AdventurerVillage.Utilities;
using UnityEngine;
using CharacterInfo = AdventurerVillage.CharacterSystem.CharacterInfo;

namespace AdventurerVillage.PartySystem
{
    [Serializable/*, ReadOnly*/]
    public class PartyInfo
    {
        #region Constructors

        public PartyInfo()
        {
        }

        public PartyInfo(int partyId)
        {
            id = partyId;
            partyName = $"Party {partyId}";
        }

        #endregion

        #region PartyInfo

        [SerializeField] private int id;
        [SerializeField] private string partyName;
        [SerializeField, ReadOnly] private List<string> characterNames = new();
        public Action OnCharacterListChanged;
        public int PartyID => id;
        public string PartyName => partyName;
        public string[] CharacterNames => characterNames.ToArray();

        public void AddCharacter(CharacterInfo character)
        {
            characterNames.Add(character.Name);
            character.OnAddedToParty(id);
            OnCharacterListChanged?.Invoke();
        }

        public void RemoveCharacter(CharacterInfo character)
        {
            if (!characterNames.Contains(character.Name)) return;
            characterNames.Remove(character.Name);
            character.OnRemovedFromParty();
            OnCharacterListChanged?.Invoke();
        }

        public void ReleaseAllCharacters()
        {
            foreach (var characterName in characterNames)
            {
                var characterExist = CharacterInfoService.Instance.TryToGetCharacterInfo(characterName, out var character);
                if (!characterExist) continue;
                character.OnRemovedFromParty();
            }

            characterNames.Clear();
        }

        #endregion

        #region Party Inventory

        private Inventory _partyInventory = new(12);
        public Inventory PartyInventory => _partyInventory;

        #endregion

        #region Party State

        [SerializeField, ReadOnly] private PartyStates currentState = PartyStates.Idle;

        public Action OnPartyStateChanged;
        public PartyStates CurrentState => currentState;

        public void ChangePartyState(PartyStates newState)
        {
            currentState = newState;
            OnPartyStateChanged?.Invoke();
        }

        #endregion
    }
}