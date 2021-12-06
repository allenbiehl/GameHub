using System.Collections.Generic;
using System.Linq;

namespace GameHub.Games.TicTacToe2D
{
    /// <summary>
    /// Class <c>GameSeries</c> represents a multi game session, played by two 
    /// opponents, and is responsible for maintaining the game state through 
    /// their game play across games. A new game series is created if the 
    /// opponents change, however the game series is not reset if the game board
    /// size or win length changes.
    /// </summary>
    public class GameSeries
    {
        /// <summary>
        /// Property <c>Games</c> is the list of games played between the two
        /// opponents.
        /// </summary>
        public List<GameState> Games { get; }

        /// <summary>
        /// Property <c>Player1</c> is the human player who started game hub.
        /// Regardless of who is the offensive player or defensive player,
        /// player 1 and player 2 always remain the same. Player 1 does not
        /// represent who made the first move. That state is maintained on the
        /// <c>GameState</c> instance as that changes between games.
        /// </summary>
        public IPlayer Player1 { get; private set; }

        /// <summary>
        /// Property <c>Player2</c> is the AI player or an alternate human player.
        /// </summary>
        public IPlayer Player2 { get; private set; }

        /// <summary>
        /// Property <c>CurrentGame</c> is the current game played if the game is
        /// in the Started state or this represents the last game if the game is 
        /// completed. 
        /// </summary>
        public GameState CurrentGame
        {
            get
            {
                if (Games.Count > 0)
                {
                    return Games.Last();
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Constructor for <c>GameSeries</c>.
        /// </summary>
        /// <param name="player1">
        /// <c>player1</c> is the primary human player.
        /// </param>
        /// <param name="player2">
        /// <c>player2</c> is the AI player or an alternate human player.
        /// </param>
        public GameSeries( IPlayer player1, IPlayer player2 )
        {
            Games = new List<GameState>();
            Player1 = player1;
            Player2 = player2;    
        }
    }
}
