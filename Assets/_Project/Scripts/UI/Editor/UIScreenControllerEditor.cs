using System.Reflection;
using AdventurerVillage.UI.ScreenControlSystems;
using UnityEditor;
using UnityEngine;

namespace AdventurerVillage.UI.Editor
{
    [CustomEditor(typeof(UIScreenController))]
    public class UIScreenControllerEditor : UnityEditor.Editor
    {
        private UIScreenController uiScreenController;
        private FieldInfo uiScreensArrayField;

        private void Awake()
        {
            uiScreenController = target as UIScreenController;
            uiScreensArrayField =
                typeof(UIScreenController).GetField("screens", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Refresh"))
            {
                SearchUIScreens();
            }

            base.OnInspectorGUI();
        }

        private void SearchUIScreens()
        {
            var screens = FindObjectsByType<UIScreen>(FindObjectsSortMode.InstanceID);
            uiScreensArrayField.SetValue(uiScreenController, screens);
            EditorUtility.SetDirty(uiScreenController);
        }
    }
}