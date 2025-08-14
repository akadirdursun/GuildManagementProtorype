using System;
using System.Globalization;
using AdventurerVillage.CustomScriptableObjectSystem;
using AdventurerVillage.Utilities;
using UnityEngine;

namespace AdventurerVillage.SaveSystem
{
    [CreateAssetMenu(fileName = "SaveLoadManager", menuName = "Adventurer Village/Save System/Save Load Manager")]
    public class SaveLoadManager : SavableCustomScriptableObject
    {
        #region Structs

        [Serializable/*, ReadOnly*/]
        public struct SaveSlotInfo
        {
            public string slotName;
            public string lastSaveTime;
        }

        #endregion

        [SerializeField, ReadOnly] private SavableScriptableObject[] savableAssets;

        [SerializeField, Space, ReadOnly/*, ListDrawerSettings(ListElementLabelName = "slotName")*/]
        private SaveSlotInfo[] savePaths = new SaveSlotInfo[3];

        private string DefaultSavePath => "SavePaths.es3";

        #region CustomScriptableObject Methods

        public override void OnGameStarted()
        {
            Load();
        }

        #endregion

        [ContextMenu("Save Game")]
        public void SaveGame()
        {
            foreach (var savable in savableAssets)
            {
                savable.Save();
            }

            Save();
        }

        [ContextMenu("Load Game")]
        public void LoadGame()
        {
            /*if (!ES3.FileExists())
            {
                Debug.LogError("Save file does not exist");
                return;
            }*/
            ResetAllScriptableObjects();
            foreach (var savable in savableAssets)
            {
                savable.Load();
            }
        }

        public void SelectSavePath(int slotIndex)
        {
            var path = GetSavePath(slotIndex);
            //ES3Settings.defaultSettings.path = path;
        }

        public SaveSlotInfo GetSaveSlotInfo(int saveIndex)
        {
            return savePaths[saveIndex];
        }

        public void CreateSaveSlot(string saveName, int saveIndex)
        {
            if (saveIndex >= savePaths.Length || string.IsNullOrEmpty(saveName)) return;

            var saveSlotInfo = new SaveSlotInfo()
            {
                slotName = saveName,
                lastSaveTime = DateTime.Now.ToString(CultureInfo.InvariantCulture)
            };
            savePaths[saveIndex] = saveSlotInfo;
        }

        public bool SaveFileExists(int index)
        {
            var savePath = GetSavePath(index);
            return index < savePaths.Length/* && ES3.FileExists(savePath)*/;
        }

        public void DeleteSavePath(int saveIndex)
        {
            if (!SaveFileExists(saveIndex)) return;
            //ES3.DeleteFile(GetSavePath(saveIndex));
        }

        public void ResetAllScriptableObjects()
        {
            foreach (var asset in savableAssets)
            {
                asset.Reset();
            }
        }

        private string GetSavePath(int saveIndex)
        {
            return $"SaveSlot_{saveIndex}.es3";
        }

        #region Savable Methods

        public override void Save()
        {
            //ES3.Save("savePaths", savePaths, DefaultSavePath);
        }

        public override void Load()
        {
            /*if (!ES3.FileExists(DefaultSavePath))
            {
                Save();
            }

            savePaths = ES3.Load("savePaths", DefaultSavePath, new SaveSlotInfo[3]);*/
        }

        #endregion
    }
}