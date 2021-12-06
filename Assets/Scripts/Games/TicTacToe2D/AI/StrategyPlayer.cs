using GameHub.Core.Security;

namespace GameHub.Games.TicTacToe2D.AI.Strategy
{
    /// <summary>
    /// Class <c>StrategyPlayer</c> represents the base AI player that is 
    /// extended based on custom AI player implementations. i.e. Beginner, Expert...
    /// </summary>
    public class StrategyPlayer : Player, IPlayer
    {
        /// <summary>
        /// Property <c>IComputerStrategy</c> is the algorithm that the AI will use
        /// to play the game. 
        /// </summary>
        protected IComputerStrategy Strategy { get; set; }

        /// <summary>
        /// Constructor for the <c>StrategyPlayer</c>.
        /// </summary>
        /// <param name="userInfo">
        /// <c>userInfo</c> is the user information associated with the player.
        /// </param>
        /// <param name="settings">
        /// <c>settings</c> is the player game settings associated with the player.
        /// </param>
        /// <param name="strategy">
        /// <c>strategy</c> is the algorith the AI player will use to play the game.
        /// </param>
        public StrategyPlayer(
            UserInfo userinfo,
            PlayerSettings settings,
            IComputerStrategy strategy = null
        ) : base(userinfo, settings)
        {
            Strategy = strategy;
        }

        /// <summary>
        /// Method <c>Play</c> is used to notify a player that it is their 
        /// turn and they need to make a game board selection. The AI executes
        /// the configured strategy to make the best possible move.
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
        public void Play(GameManager gameManager, GameSeries gameSeries, GameState gameState)
        {
            if (Strategy != null)
            {
                //var watch = new System.Diagnostics.Stopwatch();
                //watch.Start();

                PlayerMove move = this.Strategy.CalculateMove(gameSeries, gameState);

                //watch.Stop();
                //Debug.Log($"Execution Time: {watch.ElapsedMilliseconds} ms");

                gameManager.MakePlay(move);
            }
        }
    }

}
