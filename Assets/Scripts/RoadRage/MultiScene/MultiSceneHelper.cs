using System;
using UnityEngine.SceneManagement;
using AsyncOperation = UnityEngine.AsyncOperation;

namespace RoadRage.MultiScene
{
    public interface IMultiSceneManager
    {
        void LoadScene(ScenesStateEnum scene, Action sceneLoaded = null);
        void UnloadLastScene(Action sceneLoaded = null);
        void UnloadScene(ScenesStateEnum scene, Action sceneLoaded = null);
        void SetActiveScene(ScenesStateEnum scene);
        void SetActiveLastLoadScene();
    }

    public class MultiSceneManager : IMultiSceneManager
    {
        private ScenesStateEnum _lastLoadedLevel = ScenesStateEnum.Base;
        private ScenesStateEnum _lastScene = ScenesStateEnum.Base;

        public void LoadScene(ScenesStateEnum scene, Action sceneLoaded)
        {
            AsyncOperation load = SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Additive);

            load.completed += (AsyncOperation result) =>
            {
                _lastScene = _lastLoadedLevel;
                _lastLoadedLevel = scene;
                sceneLoaded?.Invoke();
            };
        }

        public void UnloadLastScene(Action sceneLoaded)
        {
            AsyncOperation load = SceneManager.UnloadSceneAsync(_lastScene.ToString());

            load.completed += (AsyncOperation result) =>
            {
                sceneLoaded?.Invoke();
            };
        }

        public void UnloadScene(ScenesStateEnum scene, Action sceneLoaded)
        {
            AsyncOperation load = SceneManager.UnloadSceneAsync(scene.ToString());

            load.completed += (AsyncOperation result) =>
            {
                sceneLoaded?.Invoke();
            };
        }

        public void SetActiveScene(ScenesStateEnum scene) =>
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene.ToString()));

        public void SetActiveLastLoadScene() =>
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(_lastLoadedLevel.ToString()));
    }
}