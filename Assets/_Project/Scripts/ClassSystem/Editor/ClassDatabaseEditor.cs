using System.Reflection;
using AdventurerVillage.Utilities;
using UnityEditor;
using UnityEngine;

namespace AdventurerVillage.ClassSystem
{
    [CustomEditor(typeof(ClassDatabase))]
    public class ClassDatabaseEditor : Editor
    {
        private FieldInfo _combatClassesField;
        private ClassDatabase _classDatabase;

        private void Awake()
        {
            _classDatabase = target as ClassDatabase;
            _combatClassesField = typeof(ClassDatabase).GetField("combatClasses", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Refresh"))
                RefreshClasses();
        }

        private void RefreshClasses()
        {
            var combatClassAssets = EditorUtilities.GetInstances<CombatClass>();
            if (combatClassAssets != null) _combatClassesField.SetValue(_classDatabase, combatClassAssets);
            EditorUtility.SetDirty(_classDatabase);
        }
    }
}