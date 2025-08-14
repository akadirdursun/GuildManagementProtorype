using System.Reflection;
using AdventurerVillage.Utilities;
using UnityEditor;
using UnityEngine;

namespace AdventurerVillage.SaveSystem
{
    [CustomEditor(typeof(SaveLoadManager))]
    public class SaveLoadManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Refresh"))
                SetSavableAssets();
        }

        private void SetSavableAssets()
        {
            var saveLoadManager= (SaveLoadManager) target;
            var savableAssets =
                typeof(SaveLoadManager).GetField("savableAssets", BindingFlags.NonPublic | BindingFlags.Instance);
            var assets = EditorUtilities.GetInstances<SavableScriptableObject>();
            if (assets != null) savableAssets.SetValue(saveLoadManager, assets);
            EditorUtility.SetDirty(saveLoadManager);
        }
    }
}