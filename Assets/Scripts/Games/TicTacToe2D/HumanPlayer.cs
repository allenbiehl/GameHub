using UnityEngine;
using GameHub.Core.Security;

namespace GameHub.Games.TicTacToe2D
{
    /// <summary>
    /// Class <c>HumanPlayer</c> represents a human and is either player 1 in a
    /// 1 player game or can be player 1 or player 2 in a 2 player game. 
    /// </summary>
    public class HumanPlayer : Player, IPlayer
    {
        /// <summary>
        /// Constructor for the <c>HumanPlayer</c>.
        /// </summary>
        /// <param name="userInfo">
        /// <c>userInfo</c> is the user information associated with the player.
        /// </param>
        /// <param name="settings">
        /// <c>settings</c> is the player game settings associated with the player.
        /// </param>
        public HumanPlayer( UserInfo userinfo, PlayerSettings settings ) : base(userinfo, settings)
        {
        }

        /// <summary>
        /// Method <c>Play</c> is used to notify a player that it is their 
        /// turn and they need to make a game board selection. Essentially
        /// this method acts as a waiting state until the user clicks on 
        /// an available game cell.
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
        public void Play( GameManager gameManager, GameSeries gameSeries, GameState game )
        {
        }
    }
}
