using UnityEngine;

namespace GameHub.Games.TicTacToe2D.UI
{
    /// <summary>
    /// Class <c>GameLoaderPanel</c> is the primary entry point for the <c>GameManager</c>
    /// and must be called before any other interaction with the <c>GameManager</c> occurs.
    /// </summary>
    public class GameLoaderPanel : MonoBehaviour
    {

        /// <summary>
        /// Method <c>Awake</c> is used to initialize / reset the <c>GameManager</c> instance.
        /// </summary>
        private void Awake()
        {
            GameManager.Initialize();
        }
    }
}
