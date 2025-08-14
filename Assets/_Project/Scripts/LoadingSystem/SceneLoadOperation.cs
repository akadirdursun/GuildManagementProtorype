using AdventurerVillage.SceneLoadSystem;
using AKD.Common.SequenceSystem;
using DevLocker.Utils;
using UnityEngine;

namespace AdventurerVillage.LoadingSystem
{
    public class SceneLoadOperation : BaseOperationBehaviour
    {
        [SerializeField] private SceneReference scene;

        public override void Begin()
        {
            SceneLoader.Instance.LoadScene(scene, Complete);
        }
    }
}