using System;
using ADK.Common;
using AdventurerVillage.LoadingSystem;
using DevLocker.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AdventurerVillage.SceneLoadSystem
{
    public class SceneLoader : Singleton<SceneLoader>
    {
        [SerializeField] private GameLoadTypeData gameLoadTypeData;
        [SerializeField] private SceneReference mainMenuScene;
        [SerializeField] private SceneReference loadingScene;
        [SerializeField] private SceneReference guildInfoSelectionScene;

        public void StartGameLoading(GameLoadType gameLoadType)
        {
            gameLoadTypeData.SetLoadType(gameLoadType);
            UnloadScene(mainMenuScene);
            LoadScene(loadingScene);
        }

        public void ReturnToMainMenuFromInGame()
        {
            var sceneCount = SceneManager.sceneCount;
            for (int i = 0; i < sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                if (scene.buildIndex == 0) continue; //Pass the master scene
                UnloadScene(scene);
            }

            LoadScene(mainMenuScene);
        }

        public void ReturnToMainMenu()
        {
            UnloadScene(guildInfoSelectionScene);
            UnloadScene(loadingScene);
            LoadScene(mainMenuScene);
        }

        public void LoadScene(SceneReference sceneReference, Action onComplete = null)
        {
            if (SceneLoaded(sceneReference)) return;
            var loadSceneAsync = SceneManager.LoadSceneAsync(sceneReference.ScenePath, LoadSceneMode.Additive);
            loadSceneAsync.completed += _ => onComplete?.Invoke();
        }

        public void UnloadScene(SceneReference sceneReference)
        {
            if (!SceneLoaded(sceneReference)) return;
            SceneManager.UnloadSceneAsync(sceneReference.ScenePath);
        }
        
        private void UnloadScene(Scene scene)
        {
            SceneManager.UnloadSceneAsync(scene);
        }

        #region Private Methods

        private bool SceneLoaded(SceneReference sceneReference)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                if (scene.path.Equals(sceneReference.ScenePath)) return true;
            }

            return false;
        }

        #endregion

        #region Monoehaviour Methods

        private void Start()
        {
            LoadScene(mainMenuScene);
        }

        #endregion
    }
}