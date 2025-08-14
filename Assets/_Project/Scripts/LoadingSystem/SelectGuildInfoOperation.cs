using AdventurerVillage.SceneLoadSystem;
using AKD.Common.SequenceSystem;
using DevLocker.Utils;
using UnityEngine;

namespace AdventurerVillage.LoadingSystem
{
    public class SelectGuildInfoOperation : BaseOperationBehaviour
    {
        [SerializeField] private SceneReference guildInfoScene;

        public override void Begin()
        {
            SceneLoader.Instance.LoadScene(guildInfoScene);
        }

        public override void Complete()
        {
            SceneLoader.Instance.UnloadScene(guildInfoScene);
            base.Complete();
        }
    }
}