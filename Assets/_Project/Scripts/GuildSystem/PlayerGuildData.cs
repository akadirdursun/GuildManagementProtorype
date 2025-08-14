using System;
using System.Linq;
using AdventurerVillage.PartySystem;
using AdventurerVillage.SaveSystem;
using AdventurerVillage.Utilities;
using UnityEngine;
using CharacterInfo = AdventurerVillage.CharacterSystem.CharacterInfo;

namespace AdventurerVillage.GuildSystem
{
    [CreateAssetMenu(fileName = "PlayerGuildData", menuName = "Adventurer Village/Guild System/Player Guild Data")]
    public class PlayerGuildData : SavableScriptableObject
    {
        [SerializeField] private AIGuildData aiGuildData;
        [SerializeField, Space, ReadOnly] private GuildInfo guildInfo;

        public string GuildName => guildInfo.GuildName;
        public PartyInfo[] AllParties => guildInfo.Parties;

        public PartyInfo[] IdleParties => guildInfo.Parties.Where(party => party.CurrentState == PartyStates.Idle).ToArray();
        public CharacterInfo[] IdleCharacters=> guildInfo.IdleCharacters;

        public Action OnPartyListChanged
        {
            get => guildInfo.OnPartyListChanged;
            set => guildInfo.OnPartyListChanged = value;
        }

        //TODO: Move create method to another script???
        public void CreateGuild(string guildName)
        {
            guildInfo = new GuildInfo(guildName);
        }

        public bool TryToChangeGuildName(string newName)
        {
            var isNameValid = aiGuildData.IsGuildNameValid(newName);
            if (isNameValid)
            {
                guildInfo.ChangeGuildName(newName);
            }

            return isNameValid;
        }

        public PartyInfo CreateNewParty()
        {
            var newId = AllParties.Any() ? AllParties.OrderBy(p => p.PartyID).Last().PartyID + 1 : 0;
            var newParty = new PartyInfo(newId);
            guildInfo.AddParty(newParty);
            OnPartyListChanged?.Invoke();
            return newParty;
        }

        public void RemoveParty(PartyInfo party)
        {
            guildInfo.RemoveParty(party);
            party.ReleaseAllCharacters();
            OnPartyListChanged?.Invoke();
        }
        
        public void AddCharacter(CharacterInfo character)
        {
            guildInfo.AddCharacter(character);
        }

        public void RemoveCharacter(CharacterInfo character)
        {
           guildInfo.RemoveCharacter(character);
        }
        
        #region Savable

        public override void Save()
        {
            //ES3.Save("guildInfo", guildInfo);
        }

        public override void Load()
        {
            //guildInfo = ES3.Load<GuildInfo>("guildInfo");
        }

        public override void Reset()
        {
            guildInfo = null;
        }

        #endregion
    }
}