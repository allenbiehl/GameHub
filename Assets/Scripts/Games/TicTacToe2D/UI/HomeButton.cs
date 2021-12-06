using UnityEngine;
using GameHub.Core;
using Zenject;

namespace GameHub.Games.TicTacToe2D.UI
{
    /// <summary>
    /// Class <c>HomeButton</c> is used by the user to navigate back to the Main Menu.
    /// </summary>
    public class HomeButton : MonoBehaviour
    {
        /// <summary>
        /// Instance variable <c>_sceneLoader</c> is used to load scenes.
        /// </summary>
        private ISceneLoader _sceneLoader;

        /// <summary>
        /// Method <c>Setup</c> is responsible for wiring up depedencies on object creation.
        /// </summary>
        /// <param name="playerSettingsService">
        /// <c>playerSettingsService</c> is used to load scenes.
        /// </param>
        [Inject]
        public void Setup(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        /// <summary>
        /// Method <c>OnLoadMainMenu</c> is executed when the user clicks the home
        /// button at which point the <c>SceneLoader</c> displays the Main Menu scene.
        /// </summary>
        public void OnLoadMainMenu()
        {
            _sceneLoader.LoadMainMenu();
        }
    }
}