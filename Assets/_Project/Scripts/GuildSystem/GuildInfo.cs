using System;
using System.Collections.Generic;
using System.Linq;
using AdventurerVillage.LevelSystem;
using AdventurerVillage.PartySystem;
using UnityEngine;
using CharacterInfo = AdventurerVillage.CharacterSystem.CharacterInfo;

namespace AdventurerVillage.GuildSystem
{
    [Serializable]
    public class GuildInfo
    {
        #region Constractors

        public GuildInfo(string guildName)
        {
            this.guildName = guildName;
        }

        public GuildInfo(string guildName, Grade guildGrade)
        {
            this.guildName = guildName;
            this.guildGrade = guildGrade;
        }

        #endregion

        [SerializeField] private string guildName;
        [SerializeField] private Grade guildGrade;
        [SerializeField] private List<CharacterInfo> guildCharacters = new();
        [SerializeField] private List<PartyInfo> parties = new();

        public string GuildName => guildName;
        public PartyInfo[] Parties => parties.ToArray();
        public CharacterInfo[] IdleCharacters => guildCharacters.Where(c => !c.IsPartyMember).ToArray();
        public Grade GuildGrade => guildGrade;
        public Action OnGuildNameChanged;
        public Action OnPartyListChanged;

        public void ChangeGuildName(string newName)
        {
            guildName = newName;
            OnGuildNameChanged?.Invoke();
        }

        public void ChangeGuildGrade(Grade newGrade)
        {
            guildGrade = newGrade;
        }

        #region Party Control

        public void AddParty(PartyInfo party)
        {
            parties.Add(party);
            OnPartyListChanged?.Invoke();
        }

        public void RemoveParty(PartyInfo party)
        {
            if (!parties.Contains(party)) return;
            //party.ClearPartyMembers();
            parties.Remove(party);
            OnPartyListChanged?.Invoke();
        }

        #endregion
        #region Character Control

        public void AddCharacter(CharacterInfo character)
        {
            if (guildCharacters.Any(c => c.Name == character.Name)) return;
            guildCharacters.Add(character);
            character.SetGuild(guildName);
        }

        public void RemoveCharacter(CharacterInfo character)
        {
            if (guildCharacters.All(c => c.Name != character.Name)) return;
            character.EmptyGuildInfo();
            guildCharacters.Remove(character);
        }

        #endregion
    }
}