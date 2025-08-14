using System.Reflection;
using AdventurerVillage.Utilities;
using UnityEditor;
using UnityEngine;

namespace AdventurerVillage.BuildingSystem
{
    [CustomEditor(typeof(BuildingDatabase))]
    public class BuildingDatabaseEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Refresh"))
            {
                Refresh();
            }
        }

        private void Refresh()
        {
            var buildingDatabase = (BuildingDatabase)target;
            var buildingDataField = typeof(BuildingDatabase).GetField("buildingData", BindingFlags.NonPublic | BindingFlags.Instance);
            var buildingDataArray = EditorUtilities.GetInstances<BuildingData>();
            if (buildingDataField == null || buildingDataArray == null) return;
            buildingDataField.SetValue(buildingDatabase, buildingDataArray);
            EditorUtility.SetDirty(buildingDatabase);
        }
    }
}