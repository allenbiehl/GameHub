using UnityEngine;

namespace GameHub.Games.TicTacToe2D.UI
{
    /// <summary>
    /// Class <c>NewSeriesButton</c> is used by the user to start a new game series.
    /// </summary>
    public class NewSeriesButton : MonoBehaviour
    {
        /// <summary>
        /// Method <c>OnLoadNewSeriesModal</c> is executed when the user clicks the 
        /// New Series button at which point the system opens the <c>NewSeriesModal</c>
        /// instance.
        /// </summary>
        public void OnLoadNewSeriesModal()
        {
            NewSeriesModal.Instance.Open();
        }
    }
}