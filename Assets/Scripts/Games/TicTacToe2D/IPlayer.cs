using GameHub.Core.Security;

namespace GameHub.Games.TicTacToe2D
{
    /// <summary>
    /// Interface <c>IPlayer</c> represents the public API common to all game 
    /// players including both human and AI players.
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        /// Property <c>UserInfo</c> is the user information associated with
        /// the player.
        /// </summary>
        UserInfo UserInfo { get; }

        /// <summary>
        /// Property <c>Settings</c> is the player game settings associated
        /// with the player.
        /// </summary>
        PlayerSettings Settings { get; }

        /// <summary>
        /// Method <c>Play</c> is used to notify a player that it is their 
        /// turn and they need to make a game board selection.
        /// </summary>
        /// <param name="gameManager">
        /// <c>gameManager</c> is provided for the player to make a play.
        /// </param>
        /// <param name="gameSeries">
        /// <c>gameSeries</c> is the current game series that includes references
        /// to both players and their game history. An advanced AI could potentially
        /// use the players history to determine player strategies.
        /// </param>
        /// <param name="gameState">
        /// <c>gameState</c> is the current game state.
        /// </param>
        void Play( GameManager gameManager, GameSeries gameSeries, GameState gameState );
    }
}