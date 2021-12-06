using GameHub.Core.Security;

namespace GameHub.Games.TicTacToe2D
{
    /// <summary>
    /// Class <c>Player</c> is the base abstract class that provides common 
    /// functionality for all player types including human and AI.
    /// </summary>
    public abstract class Player
    {
        /// <summary>
        /// Property <c>UserInfo</c> is the user information associated with
        /// the player.
        /// </summary>
        public UserInfo UserInfo { get; private set; }

        /// <summary>
        /// Property <c>Settings</c> is the player game settings associated
        /// with the player.
        /// </summary>
        public PlayerSettings Settings { get; private set; }

        /// <summary>
        /// Constructor for the <c>Player</c> abstract class.
        /// </summary>
        /// <param name="userInfo">
        /// <c>userInfo</c> is the user information associated with the player.
        /// </param>
        /// <param name="settings">
        /// <c>settings</c> is the player game settings associated with the player.
        /// </param>
        public Player( UserInfo userInfo, PlayerSettings settings )
        {
            UserInfo = userInfo;
            Settings = settings;
        }
    }
}
