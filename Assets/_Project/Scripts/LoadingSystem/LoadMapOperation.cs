using AdventurerVillage.HexagonGridSystem;
using AdventurerVillage.SceneLoadSystem;
using AKD.Common.SequenceSystem;
using DevLocker.Utils;
using UnityEngine;

namespace AdventurerVillage.LoadingSystem
{
    public class LoadMapOperation : BaseOperationBehaviour
    {
        [SerializeField] private GameLoadTypeData gameLoadTypeData;
        [SerializeField] private HexMapData hexMapData;
        [SerializeField] private SceneReference mapScene;

        public override void Begin()
        {
            switch (gameLoadTypeData.LoadType)
            {
                case GameLoadType.LoadNewGame:
                    CreateNewMap();
                    break;
                case GameLoadType.LoadPreviousGame:
                    LoadMap();
                    break;
            }
        }

        private void CreateNewMap()
        {
            hexMapData.CreateRandomMapConfig();
            LoadMap();
        }

        private void LoadMap()
        {
            SceneLoader.Instance.LoadScene(mapScene);
        }
    }
}