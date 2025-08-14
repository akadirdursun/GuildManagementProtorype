using System;
using ADK.Common;
using AdventurerVillage.LevelSystem;
using UnityEngine;

namespace AdventurerVillage.RaidSystem.RaidAreaControllers
{
    public class RaidGateAreaController : Singleton<RaidGateAreaController>
    {
        [SerializeField] private RaidData raidData;
        [SerializeField] private GradeTable gradeTable;
        [SerializeField] private MeshRenderer gateRenderer;
        [SerializeField] private GameObject[] components;

        public void Enable()
        {
            var gateInfo = raidData.TargetGateInfo;
            var gateColor = gradeTable.GetColor(gateInfo.Grade);
            gateRenderer.material.color = gateColor;
            foreach (var component in components)
            {
                component.SetActive(true);
            }
        }

        public void Disable()
        {
            foreach (var component in components)
            {
                component.SetActive(false);
            }
        }

        #region MonoBehaviour Methods

        private void Start()
        {
            Disable();
        }

        #endregion
    }
}