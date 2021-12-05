using GameHub.Core;
using Zenject;

namespace GameHub.Games.TicTacToe2D
{
    /// <summary>
    /// Class <c>PlayerSettingsManager</c> provides the ability to load and save
    /// <c>PlayerSettings</c> that should persist across game sessions. For example,
    /// the starting game board size used in the previous game session.
    /// </summary>
    /// <example>
    /// PlayerSettings settings = PlayerSettingsManager.Instance.LoadSettings();
    /// settings = PlayerSettingsManager.Instance.GetSettings();
    /// PlayerSettingsManager.Instance.SaveSettings(settings);
    /// </example>
    public class PlayerSettingsService : IPlayerSettingsService
    {
        /// <summary>
        /// Instance variable <c>_gameSettingsService</c> is used to retrieve and 
        /// persist game settings.
        /// </summary>
        private IGameSettingsService _gameSettingsService;

        /// <summary>
        /// Constructor for <c>PlayerSettingsService</c>
        /// </summary>
        /// <param name="gameSettingsService">
        /// <c>gameSettingsService</c> is used to retrieve and persist game settings
        /// and an underlying data store.
        /// </param>
        [Inject]
        public PlayerSettingsService(IGameSettingsService gameSettingsService)
        {
            _gameSettingsService = gameSettingsService;
        }

        /// <summary>
        /// Method <c>SaveSettings</c> is used to serialize the <c>PlayerSettings</c> and
        /// write the associated data to the underlying data store.
        /// </summary>
        /// <param name="userInfo">
        /// <c>settings</c> represents the <c>PlayerSettings</c> to save / write to the 
        /// underylying data store.
        /// </param>
        public void SaveSettings(PlayerSettings settings)
        {
            if (settings.BoardSize < 3)
            {
                return;
            }
            if (settings.LengthToWin < 3)
            {
                return;
            }

            _gameSettingsService.SaveSettings("TicTacToe2D", settings);
        }

        /// <summary>
        /// Method <c>LoadSettings</c> is used to retrieve the <c>PlayerSettings</c> associated
        /// with the current user directly from the underyling data store, bypassing
        /// any cached instance. Typically you should call <c>GetSettings</c> to speed
        /// up retrieval of the cached <c>PlayerSettings</c>.
        /// </summary>
        /// <returns>
        /// Returns instance of <c>PlayerSettings</c> associated with the current user.
        /// </returns>
        public PlayerSettings GetSettings()
        {
            PlayerSettings settings = _gameSettingsService.GetSettings<PlayerSettings>("TicTacToe2D");
            return (settings == null) ? PlayerSettings.Alpha : settings;
        }
    }
}