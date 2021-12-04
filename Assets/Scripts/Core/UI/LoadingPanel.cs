using UnityEngine;

namespace GameHub.Core.UI
{
    /// <summary>
    /// Class <c>LoadingPanel</c> represents the main "loading" panel that is 
    /// used to transition between the main game panel and the selected game.
    /// </summary>
    public class LoadingPanel : MonoBehaviour
    {
        /// <summary>
        /// Instance variable <c>_isSceneLoaded</c> is used as a flag to determine 
        /// when the loader has fully loaded. 
        /// </summary>
        private bool _isSceneLoaded = false;

        /// <summary>
        /// Method <c>Update</c> is used to check when the scene is fully loaded. 
        /// Once the scene is fully loaded then we notify the scene loader that 
        /// we can transition to the target scene.
        /// </summary>
        void Update()
        {
            if (!_isSceneLoaded)
            {
                SceneLoader.Instance.LoaderCallback();
                _isSceneLoaded = true;
            }
        }
    }
}
