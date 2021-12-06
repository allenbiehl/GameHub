using GameHub.Games.TicTacToe2D.Event;

namespace GameHub.Games.TicTacToe2D
{
    /// <summary>
    /// Interface <c>IGameManager</c> represents the base interface 
    /// for all <c>GameManager</c> implementations.
    /// </summary>
    public interface IGameManager
    {
        /// <summary>
        /// Method <c>GetGameSeries</c> is used to return the active game series,
        /// which is a collection of games played between two opponents.
        /// </summary>
        /// <returns>
        /// <c>GameSeries</c> is the current game series being played.
        /// </returns>
        GameSeries GetGameSeries();

        /// <summary>
        /// Method <c>GetEventBus</c> provides channels for notifying listeners
        /// when particular events occur. 
        /// </summary>
        /// <returns>
        /// <c>EventBus</c> is responsible for centrally managing event channels.
        /// </returns>
        EventBus GetEventBus();

        /// <summary>
        /// Method <c>GetCurrentGame</c> is used to return the active game being
        /// played or the last game played if no new game was started.
        /// </summary>
        /// <returns>
        /// <c>GameState</c> is the instance of the current game in play or last 
        /// completed.
        /// </returns>
        GameState GetCurrentGame();

        /// <summary>
        /// Method <c>StartSeries</c> is used to start a new game series between
        /// two opponents. This resets wins and ties back to 0.
        /// </summary>
        /// <param name="player1">
        /// <c>player1</c> represents the player who started game hub and is always
        /// a human player.
        /// </param>
        /// <param name="player2">
        /// <c>player2</c> represents the selected opponent of player 1. This could
        /// either be another human player that uses the same mouse to choose open
        /// cells or an AI.
        /// </param>
        /// <returns>
        /// <c>GameSeries</c> is the current game series which contains references 
        /// of both opponents as well as the game history.
        /// </returns>
        GameSeries StartSeries(IPlayer player1, IPlayer player2);

        /// <summary>
        /// Method <c>InitializeGame</c> is used to create a new game, but leave the
        /// game in a pending state until external events occurs, such as building
        /// the game board, or resetting game series stats. We make sure that all
        /// outcomes from the previous game are reset.
        /// </summary>
        /// <param name="gameBoard">
        /// <c>gameBoard</c> is the the virtual game state board with a reference to
        /// all game board cells. This is used to determine board dimensions and cell
        /// coordinates.
        /// </param>
        /// <param name="lengthToWin">
        /// <c>lengthToWin</c>represents the total number of sequential cells claimed 
        /// by a single player that are required to win. In tic tac toe the standard
        /// length is 3.
        /// </param>
        /// <returns></returns>
        GameState InitializeGame(GameBoard gameBoard, int lengthToWin);

        /// <summary>
        /// Method <c>StartGame</c> is used to start the game after all UI operations
        /// have occured. Once the game is started, the <c>GameManager</c> determines
        /// the offensive and defensive player for the current game and then notifies 
        /// the player that they need to take their turn.
        /// </summary>
        void StartGame();

        /// <summary>
        /// Method <c>MakePlay</c> represents the callback for a player to specify 
        /// which move they wish to make. The <c>GameManager</c> then determines
        /// whether the move is valid, whether its this person's turn, and other checks
        /// prior to the player claiming the game board cell.
        /// </summary>
        /// <param name="playerMove">
        /// <c>playerMove</c> is the combination of the play and game cell coordinates 
        /// associated with a players turn. Using player moves we can track the path of
        /// how users / opponents play.
        /// </param>
        void MakePlay(PlayerMove playerMove);
    }
}
