using System.Collections.Generic;
using System.Reflection;
using AdventurerVillage.ItemSystem.Equipments;
using AdventurerVillage.Utilities;
using UnityEditor;
using UnityEngine;

namespace AdventurerVillage.ItemSystem
{
    [CustomEditor(typeof(EquipmentDatabase))]
    public class EquipmentDatabaseEditor : Editor
    {
        private FieldInfo _equipmentDataArrayField;
        private FieldInfo _equipmentIdField;
        private EquipmentDatabase _equipmentDatabase;

        private void Awake()
        {
            _equipmentDatabase = target as EquipmentDatabase;
            _equipmentDataArrayField = typeof(EquipmentDatabase)
                .GetField("equipmentDataArray", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Refresh"))
                Refresh();
            if (GUILayout.Button("Check Equipment Ids"))
                CheckEquipmentIds();
        }

        private void Refresh()
        {
            var equipmentDataArray = EditorUtilities.GetInstances<EquipmentData>();
            _equipmentDataArrayField.SetValue(_equipmentDatabase, equipmentDataArray);
            EditorUtility.SetDirty(_equipmentDatabase);
        }

        private void CheckEquipmentIds()
        {
            EquipmentData[] equipments = _equipmentDataArrayField.GetValue(_equipmentDatabase) as EquipmentData[];
            if (equipments == null) return;
            var ids = new HashSet<string>();
            foreach (var equipmentData in equipments)
            {
                if (ids.Contains(equipmentData.Id))
                {
                    typeof(Item).GetMethod("SetItemId", BindingFlags.NonPublic | BindingFlags.Instance)
                        .Invoke(equipmentData, null);
                    EditorUtility.SetDirty(equipmentData);
                }

                ids.Add(equipmentData.Id);
            }
        }
    }
}