using UnityEngine;

namespace GameHub.Core.UI
{
    public class LoadingPanel : MonoBehaviour
    {
        private bool _isSceneLoaded = false;

        void Update()
        {
            if (!_isSceneLoaded)
            {
                SceneLoader.LoaderCallback();
                _isSceneLoaded = true;
            }
        }
    }
}
