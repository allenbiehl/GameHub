using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace GameHub.Core
{
    /// <summary>
    /// Class <c>SceneLoader</c> </c> provides ability to switch
    /// between two scenes using an intermediary loader scene.
    /// </summary>
    public class SceneLoader : ISceneLoader
    {
        /// <summary>
        /// Instance variable <c>_onLoaderCallback</c> represents
        /// a callback that loads the intended view.
        /// 
        /// TODO Find a way to share state across scene contexts.
        /// Workaround is to make this static.
        /// </summary>
        private static Action _onLoaderCallback;

        /// <summary>
        /// Method <c>Load</c> is used to load standard scenes defined
        /// in the <c>Scene</c> enumeration.
        /// </summary>
        /// <param name="scene">
        /// <c>scene</c> represents the standard scene to load
        /// </param>
        /// <c>transition</c> represents whether the intermediary loading 
        /// scene should be loaded before the target scene.
        /// </param>
        public void Load(SceneType scene, bool transition = false)
        {
            Load(scene.ToString(), transition);
        }

        /// <summary>
        /// Method <c>Load</c> is used to load any scene by its name.
        /// </summary>
        /// <param name="scene">
        /// <c>scene</c> represents the scene name to load.
        /// </param>
        /// <param name="transition">
        /// <c>transition</c> represents whether the intermediary loading 
        /// scene should be loaded before the target scene.
        /// </param>
        public void Load(string scene, bool transition = false)
        {
            int sceneIndex = SceneUtility.GetBuildIndexByScenePath($"Scenes/{scene}");

            if (sceneIndex >= 0)
            {
                if (transition)
                {
                    _onLoaderCallback = () =>
                    {
                        SceneManager.LoadScene(scene);
                    };
                    SceneManager.LoadScene(SceneType.Loading.ToString());
                }
                else
                {
                    SceneManager.LoadScene(scene);
                }
            }
        }

        /// <summary>
        /// Method <c>LoadMainMenu</c> is shortcut for loading the main menu.
        /// This is equivalent to <c>SceneLoader.Load(SceneLoader.Scene.MainMenu)</c>.
        /// </summary>
        /// <param name="scene">
        /// <c>scene</c> represents the standard scene to load
        /// </param>
        public void LoadMainMenu()
        {
            Load(SceneType.MainMenu);
        }

        /// <summary>
        /// Method <c>LoaderCallback</c> is used by the <c>Loading</c>
        /// scene when loading is completed and is ready to switch to
        /// the target scene.
        /// </summary>
        public void LoaderCallback()
        {
            if (_onLoaderCallback != null)
            {
                // Temporary until we add async scene loading
                ExecuteDelayedCallback(1, () =>
                {
                    _onLoaderCallback();
                    _onLoaderCallback = null;
                });
            }
        }

        /// <summary>
        /// Method <c>ExecuteDelayedCallback</c> is used to delay execution of
        /// a callback function for the total <c>seconds</c> specified. This is
        /// a temporary solution that allows us to emulate async scene loading.
        /// </summary>
        /// <param name="seconds">
        /// <c>seconds</c> represents the total number of seconds to wait until 
        /// executing the callback function.
        /// </param>
        /// <param name="callback">
        /// <c>callback</c> represents the callback function to execute after
        /// the delay completes.
        /// </param>
        private async void ExecuteDelayedCallback(int seconds, Action callback)
        {
            await Task.Delay(TimeSpan.FromSeconds(seconds));
            callback();
        }
    }
}