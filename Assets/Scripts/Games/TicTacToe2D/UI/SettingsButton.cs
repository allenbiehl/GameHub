using UnityEngine;

namespace GameHub.Games.TicTacToe2D.UI
{
    /// <summary>
    /// Class <c>SettingsButton</c> is used by the user to open the player settings modal.
    /// </summary>
    public class SettingsButton : MonoBehaviour
    {
        /// <summary>
        /// Method <c>OnOpenSettingsModal</c> is executed when the user clicks the 
        /// Settings button at which point the system opens the <c>SettingsModal</c>
        /// instance.
        /// </summary>
        public void OnOpenSettingsModal()
        {
            SettingsModal.Instance.Open(OnSave);
        }

        /// <summary>
        /// Method <c>OnSave</c> is used to handle any followup actions after the settings
        /// are saved.
        /// </summary>
        private void OnSave()
        {

        }
    }
}