using AdventurerVillage.LevelSystem;
using UnityEngine;

namespace AdventurerVillage.GateSystem
{
    public class Gate : MonoBehaviour
    {
        [SerializeField] private GradeTable gradeTable;
        [SerializeField] private MeshRenderer meshRenderer;
        
        private GateInfo _gateInfo;

        public void Initialize(GateInfo gateInfo)
        {
            _gateInfo = gateInfo;
            var gateColor = gradeTable.GetColor(gateInfo.Grade);
            meshRenderer.material.color = gateColor;
        }
    }
}