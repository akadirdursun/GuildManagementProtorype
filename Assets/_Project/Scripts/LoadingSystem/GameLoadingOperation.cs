using System;
using AdventurerVillage.SceneLoadSystem;
using AKD.Common.SequenceSystem;
using DevLocker.Utils;
using UnityEngine;

namespace AdventurerVillage.LoadingSystem
{
    public class GameLoadingOperation : MonoBehaviour
    {
        [SerializeField] private GameLoadTypeData gameLoadTypeData;
        [SerializeField/*, BoxGroup("Operations")*/] private OperationSequence loadNewGameOperationSequence;
        [SerializeField/*, BoxGroup("Operations")*/] private OperationSequence loadPreviousOperationSequence;
        [SerializeField] private SceneReference loadingScene;

        private OperationSequence GetSelectedSequence()
        {
            return gameLoadTypeData.LoadType switch
            {
                GameLoadType.LoadNewGame => loadNewGameOperationSequence,
                GameLoadType.LoadPreviousGame => loadPreviousOperationSequence,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void UnloadLoadingScene()
        {
            SceneLoader.Instance.UnloadScene(loadingScene);
        }

        #region MonoBehaviour Methods

        private void Awake()
        {
            var sequence = GetSelectedSequence();
            sequence.onComplete.AddListener(UnloadLoadingScene);
            sequence.Init();
        }

        private void Start()
        {
            GetSelectedSequence().Begin();
        }

        #endregion
    }
}