using System.Collections.Generic;
using AdventurerVillage.SaveSystem;
using UnityEngine;

namespace AdventurerVillage.GateSystem
{
    [CreateAssetMenu(fileName = "GateDatabase", menuName = "Adventurer Village/Gate System/Gate Database")]
    public class GateDatabase : SavableScriptableObject
    {
        private List<GateInfo> _gateInfos=new ();

        public GateInfo[] GateInfos => _gateInfos.ToArray();

        public void AddGateInfo(GateInfo gateInfo)
        {
            _gateInfos.Add(gateInfo);
        }

        #region Savable Methods

        public override void Save()
        {
            //ES3.Save("gateInfos", _gateInfos);
        }

        public override void Load()
        {
            //_gateInfos = ES3.Load("gateInfos", new List<GateInfo>());
        }

        public override void Reset()
        {
            _gateInfos = new();
        }

        #endregion
    }
}