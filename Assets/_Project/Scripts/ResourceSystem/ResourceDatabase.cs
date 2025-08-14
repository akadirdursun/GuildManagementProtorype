using System.Collections.Generic;
using AdventurerVillage.SaveSystem;
using AdventurerVillage.Utilities;
using UnityEngine;

namespace AdventurerVillage.ResourceSystem
{
    [CreateAssetMenu(fileName = "ResourceDatabase", menuName = "Adventurer Village/Resource System/Resource Database")]
    public class ResourceDatabase : SavableScriptableObject
    {
        [SerializeField, ReadOnly] private ResourceData[] resources;

        private Dictionary<string, ResourceSave> _resourceSaves;

        public void Initialize()
        {
            _resourceSaves ??= new Dictionary<string, ResourceSave>();
            foreach (var resourceData in resources)
            {
                if (!_resourceSaves.TryGetValue(resourceData.ID, out var resourceSave))
                {
                    resourceData.Initialize();
                    _resourceSaves.Add(resourceData.ID, new ResourceSave()
                    {
                        amount = resourceData.Amount,
                        gainPerHour = resourceData.GainPerHour
                    });
                    continue;
                }

                resourceData.Initialize(resourceSave.amount, resourceSave.gainPerHour);
            }
        }

        private void ReadResourceAmount()
        {
            foreach (var resourceData in resources)
            {
                if (!_resourceSaves.TryGetValue(resourceData.ID, out var resourceSave))
                {
                    resourceData.Initialize();
                    resourceSave = new ResourceSave();
                }

                resourceSave.amount = resourceData.Amount;
                resourceSave.gainPerHour = resourceData.GainPerHour;
            }
        }

        #region Save Methods

        public override void Save()
        {
            ReadResourceAmount();
            //ES3.Save("ResourceSaves", _resourceSaves);
        }

        public override void Load()
        {
            //_resourceSaves = ES3.Load("ResourceSaves", new Dictionary<string, ResourceSave>());
        }

        public override void Reset()
        {
            _resourceSaves = new();
        }

        #endregion
    }
}