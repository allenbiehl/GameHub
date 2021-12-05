using UnityEngine;
using GameHub.Core;

namespace GameHub.Games.TicTacToe2D.UI
{
    /// <summary>
    /// Class <c>HomeButton</c> is used by the user to navigate back to the Main Menu.
    /// </summary>
    public class HomeButton : MonoBehaviour
    {
        /// <summary>
        /// Method <c>OnLoadMainMenu</c> is executed when the user clicks the home
        /// button at which point the <c>SceneLoader</c> displays the Main Menu scene.
        /// </summary>
        public void OnLoadMainMenu()
        {
            SceneLoader.Instance.LoadMainMenu();
        }
    }
}