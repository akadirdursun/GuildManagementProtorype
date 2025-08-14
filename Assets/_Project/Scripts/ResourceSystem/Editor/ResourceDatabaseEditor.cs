using System.Reflection;
using AdventurerVillage.Utilities;
using UnityEditor;
using UnityEngine;

namespace AdventurerVillage.ResourceSystem
{
    [CustomEditor(typeof(ResourceDatabase))]
    public class ResourceDatabaseEditor : Editor
    {
        private FieldInfo _resourcesField;
        private ResourceDatabase _resourceDatabase;
        private const string ResourceFolderPath = "Assets/_Project/ScriptableObjects/ResourceSystem/Resources/";

        private void Awake()
        {
            _resourceDatabase = (ResourceDatabase)target;
            _resourcesField =
                typeof(ResourceDatabase).GetField("resources", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Refresh Resources"))
                RefreshResources();

            if (GUILayout.Button("Create New Resource"))
                CreateNewResource();
        }

        private void RefreshResources()
        {
            var resources = EditorUtilities.GetInstances<ResourceData>();
            _resourcesField.SetValue(_resourceDatabase, resources);
            EditorUtility.SetDirty(_resourceDatabase);
        }

        private void CreateNewResource()
        {
            var newResourceData = CreateInstance<ResourceData>();
            AssetDatabase.CreateAsset(newResourceData, $"{ResourceFolderPath}NewResourceData.asset");
            AssetDatabase.SaveAssets();
            RefreshResources();
            Selection.activeObject = newResourceData;
        }
    }
}