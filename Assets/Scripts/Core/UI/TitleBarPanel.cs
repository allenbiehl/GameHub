using UnityEngine;
s
namespace GameHub.Core.UI
{
    /// <summary>
    /// Class <c>TitleBarPanel</c> is a prefab that is used across all scenes
    /// that require a title bar. This includes the logo, page title, and an
    /// exit application button. 
    /// </summary>
    public class TitleBarPanel : MonoBehaviour
    {
        /// <summary>
        /// Method <c>OnApplicationQuit</c> is executed when the user clicks
        /// the exit button.
        /// </summary>
        public void OnApplicationQuit()
        {
            Application.Quit();
        }
    }
}
