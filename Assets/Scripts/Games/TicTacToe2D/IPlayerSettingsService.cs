
namespace GameHub.Games.TicTacToe2D
{
    /// <summary>
    /// Interface <c>IPlayerSettingsService</c> represents the base interface 
    /// for all <c>PlayerSettingsService</c> implementations.
    /// </summary>
    public interface IPlayerSettingsService
    {
        /// <summary>
        /// Method <c>SaveSettings</c> is used to serialize the <c>PlayerSettings</c> and
        /// write the associated data to the underlying data store.
        /// </summary>
        /// <param name="userInfo">
        /// <c>settings</c> represents the <c>PlayerSettings</c> to save / write to the 
        /// underylying data store.
        /// </param>
        public void SaveSettings(PlayerSettings settings);

        /// <summary>
        /// Method <c>LoadSettings</c> is used to retrieve the <c>PlayerSettings</c> associated
        /// with the current user directly from the underyling data store, bypassing
        /// any cached instance. Typically you should call <c>GetSettings</c> to speed
        /// up retrieval of the cached <c>PlayerSettings</c>.
        /// </summary>
        /// <returns>
        /// Returns instance of <c>PlayerSettings</c> associated with the current user.
        /// </returns>
        public PlayerSettings GetSettings();
    }
}
