using System.Collections.Generic;
using System.Linq;
using AdventurerVillage.NameGenerationSystem;
using AdventurerVillage.SaveSystem;
using AdventurerVillage.Utilities;
using UnityEngine;

namespace AdventurerVillage.GuildSystem
{
    [CreateAssetMenu(fileName = "AIGuildData", menuName = "Adventurer Village/Guild System/AI Guild Data")]
    public class AIGuildData : SavableScriptableObject
    {
        [SerializeField] private GuildNameGenerator guildNameGenerator;
        [SerializeField, ReadOnly] public List<GuildInfo> guildInfos = new();

        private string[] GuildNames => guildInfos.ConvertAll(g => g.GuildName).ToArray();

        public GuildInfo CreateNewGuild(string guildName = "")
        {
            if (string.IsNullOrEmpty(guildName))
                guildName = guildNameGenerator.GetRandomGuildName(GuildNames);

            var newGuild = new GuildInfo(guildName);
            guildInfos.Add(newGuild);
            return newGuild;
        }

        public bool IsGuildNameValid(string guildName)
        {
            return guildInfos.All(g => g.GuildName != guildName);
        }

        #region Savable Methods

        public override void Save()
        {
            //ES3.Save("guildInfos", guildInfos);
        }

        public override void Load()
        {
            //guildInfos = ES3.Load("guildInfos", new List<GuildInfo>());
        }

        public override void Reset()
        {
            guildInfos = new();
        }

        #endregion
    }
}