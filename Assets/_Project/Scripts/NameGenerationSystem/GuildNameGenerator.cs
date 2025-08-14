using System.Linq;
using AdventurerVillage.Utilities;
using UnityEngine;

namespace AdventurerVillage.NameGenerationSystem
{
    [CreateAssetMenu(fileName = "GuildNameGenerator", menuName = "Adventurer Village/Name Generation System/Guild Name Generator")]
    public class GuildNameGenerator : ScriptableObject
    {
        [SerializeField, Space, ReadOnly/*, ListDrawerSettings(ShowIndexLabels = true)*/] private string[] guildNames;

        public string GetRandomGuildName(string[] unavailableGuildNames)
        {
            var guildName = "";
            var availableNames = guildNames.Except(unavailableGuildNames).ToArray();
            var randomIndex = 0;
            if (availableNames.Length > 0)
            {
                randomIndex = Random.Range(0, availableNames.Length);
                guildName = availableNames[randomIndex];
            }
            else
            {
                randomIndex=Random.Range(0, unavailableGuildNames.Length);
                guildName=unavailableGuildNames[randomIndex];
                var iteratedNameCount = unavailableGuildNames.Count(n => n.Contains(guildName));
                guildName += $" {iteratedNameCount + 1}";
            }

            return guildName;
        }


#if UNITY_EDITOR
        [SerializeField/*, FilePath*/, Space] private string filePath;

        [ContextMenu("Get Guild Names")]
        private void GetGuildNames()
        {
            var asset = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>(filePath);
            guildNames = JsonUtility.FromJson<NameValues>(asset.text).values;
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
    }
}