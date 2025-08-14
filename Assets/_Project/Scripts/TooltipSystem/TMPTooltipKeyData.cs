using System;
using System.Collections.Generic;
using AdventurerVillage.CustomScriptableObjectSystem;
using AdventurerVillage.TooltipSystem.UI;
using UnityEngine;

namespace AdventurerVillage.TooltipSystem
{
    [CreateAssetMenu(fileName = "TMPTooltipKeyData",
        menuName = "Adventurer Village/Tooltip System/TMP Tooltip Key Data")]
    public class TMPTooltipKeyData : CustomScriptableObject
    {
        [SerializeField/*, ListDrawerSettings(AddCopiesLastElement = true, ListElementLabelName = "title"), Searchable*/]
        private TMPTooltipLinkInfo<string>[] tooltipLinkInfos;

        [Space]
        [SerializeField/*, ListDrawerSettings(AddCopiesLastElement = true, ListElementLabelName = "title"), Searchable*/]
        private TMPTooltipLinkInfo<int>[] tooltipCharacterInfos;

        private Dictionary<string, TMPTooltipLinkInfo<string>> _tooltipLinkInfoDictionary;
        private Dictionary<int, TMPTooltipLinkInfo<int>> _tooltipCharacterInfoDictionary;

        public override void OnGameStarted()
        {
            _tooltipLinkInfoDictionary = new();
            foreach (var linkInfo in tooltipLinkInfos)
            {
                _tooltipLinkInfoDictionary.Add(linkInfo.key, linkInfo);
            }

            _tooltipCharacterInfoDictionary = new();
            foreach (var characterInfo in tooltipCharacterInfos)
            {
                _tooltipCharacterInfoDictionary.Add(characterInfo.key, characterInfo);
            }
        }

        public bool TryToGetTooltipLinkInfo(string key, out TMPTooltipLinkInfo<string> tooltipInfo)
        {
            return _tooltipLinkInfoDictionary.TryGetValue(key, out tooltipInfo);
        }

        public bool TryToGetTooltipCharacterInfo(int key, out TMPTooltipLinkInfo<int> tooltipInfo)
        {
            return _tooltipCharacterInfoDictionary.TryGetValue(key, out tooltipInfo);
        }
    }

    [Serializable]
    public struct TMPTooltipLinkInfo<T>
    {
        public T key;
        public TooltipView prefab;
        public string title;
        [TextArea] public string description;
    }
}