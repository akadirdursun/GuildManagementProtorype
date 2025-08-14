using System.Collections.Generic;
using AdventurerVillage.SaveSystem;
using UnityEngine;

namespace AdventurerVillage.CraftingSystem
{
    [CreateAssetMenu(fileName = "CraftingInfoSaveData", menuName = "Adventurer Village/Crafting System/Crafting Info Save Data")]
    public class CraftingInfoSaveData : SavableScriptableObject
    {
        private List<CraftingInfo> _ongoingCraftingInfos = new();
        private List<CraftingInfo> _completedCraftingInfos = new();
        
        public CraftingInfo[] OngoingCraftingInfos => _ongoingCraftingInfos.ToArray();
        public CraftingInfo[] CompletedCraftingInfos => _completedCraftingInfos.ToArray();

        public void AddCraftingInfo(CraftingInfo craftingInfo)
        {
            _ongoingCraftingInfos.Add(craftingInfo);
        }

        public void OnCraftingCompleted(CraftingInfo craftingInfo)
        {
            if (!_ongoingCraftingInfos.Contains(craftingInfo)) return;
            _ongoingCraftingInfos.Remove(craftingInfo);
            _completedCraftingInfos.Add(craftingInfo);
        }

        public void RemoveCompletedCraftingInfo(CraftingInfo craftingInfo)
        {
            if (!_completedCraftingInfos.Contains(craftingInfo)) return;
            _completedCraftingInfos.Remove(craftingInfo);
        }

        #region Savable

        public override void Save()
        {
            //ES3.Save("ongoingCraftingInfos", _ongoingCraftingInfos);
            //ES3.Save("completedCraftingInfos", _completedCraftingInfos);
        }

        public override void Load()
        {
            //_ongoingCraftingInfos = ES3.Load("ongoingCraftingInfos", new List<CraftingInfo>());
            //_completedCraftingInfos = ES3.Load("completedCraftingInfos", new List<CraftingInfo>());
        }

        public override void Reset()
        {
            _ongoingCraftingInfos = new();
            _completedCraftingInfos = new();
        }

        #endregion
    }
}