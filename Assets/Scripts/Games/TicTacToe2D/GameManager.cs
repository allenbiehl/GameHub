using System;
using System.Linq;
using System.Collections.Generic;
using GameHub.Games.TicTacToe2D.Event;
using GameHub.Games.TicTacToe2D.AI;
using Zenject;

namespace GameHub.Games.TicTacToe2D
{
    /// <summary>
    /// Class <c>GameManager</c> represents the core service for managing 
    /// all game operations and game session state.
    /// </summary>
    public class GameManager : IGameManager, IDisposable
    {
        /// <summary>
        /// Instance variable <c>_gameSeries</c> is used to store the current
        /// game series between two opponents. Each game series contains 0 or
        /// more game state instances which represent the historical game play
        /// between these two opponents.
        /// </summary>
        private GameSeries _gameSeries;

        /// <summary>
        /// Instance variable <c>_eventBus</c> is used to manage game event
        /// channels. Any entity can subscribe to an event and when that event
        /// occurs, then that entity will be notified that the event occurred.
        /// </summary>
        private IEventBus _eventBus;

        /// <summary>
        /// Instance variable <c>_playerPathTriangulation</c> is used to check
        /// whether thelast play a player made is a winning move. Rather than
        /// scanning each cell, we calculate the winning move based on the last
        /// play made. Using this method we can significantly check winning moves
        /// faster the scanning the entire board.
        /// </summary>
        private PlayerPathTriangulation _playerPathTriangulation;

        /// <summary>
        /// Constructor for the <c>GameManager</c>.
        /// </summary>
        [Inject]
        private GameManager(IEventBus eventBus)
        {
            _eventBus = eventBus;
            _eventBus.BoardClickEvents.AddListener(OnBoardClick);
            _playerPathTriangulation = new PlayerPathTriangulation();
        }

        /// <summary>
        /// Method <c>Dispose</c> is called when the class is destroyed and we need to 
        /// clean up dependencies.
        /// </summary>
        public void Dispose()
        {
            _eventBus.BoardClickEvents.RemoveListener(OnBoardClick);
        }

        /// <summary>
        /// Method <c>GetGameSeries</c> is used to return the active game series,
        /// which is a collection of games played between two opponents.
        /// </summary>
        /// <returns>
        /// <c>GameSeries</c> is the current game series being played.
        /// </returns>
        public GameSeries GetGameSeries()
        {
            return _gameSeries;
        }

        /// <summary>
        /// Method <c>GetCurrentGame</c> is used to return the active game being
        /// played or the last game played if no new game was started.
        /// </summary>
        /// <returns>
        /// <c>GameState</c> is the instance of the current game in play or last 
        /// completed.
        /// </returns>
        public GameState GetCurrentGame()
        {
            GameState game = null;

            if (_gameSeries != null)
            {
                game = _gameSeries.CurrentGame;
            }
            return game;
        }

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
        public GameSeries StartSeries(IPlayer player1, IPlayer player2)
        {
            _gameSeries = new GameSeries(player1, player2);
            return _gameSeries;
        }

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
        public GameState InitializeGame(GameBoard gameBoard, int lengthToWin)
        {
            // Must start series first
            if (GetGameSeries() == null)
            {
                return null;
            }

            IPlayer initialPlayer;

            // No games played, Principle starts
            if (GetGameSeries().Games.Count == 0)
            {
                initialPlayer = GetGameSeries().Player1;
            }
            // Switch initial player from last game
            else
            {
                IPlayer lastPlayer = GetGameSeries().Games.Last().InitialPlayer;

                if (lastPlayer == GetGameSeries().Player1)
                {
                    initialPlayer = GetGameSeries().Player2;
                }
                else
                {
                    initialPlayer = GetGameSeries().Player1;
                }
            }

            GameState state = new GameState(gameBoard, initialPlayer, lengthToWin);
            GetGameSeries().Games.Add(state);
            return state;
        }

        /// <summary>
        /// Method <c>StartGame</c> is used to start the game after all UI operations
        /// have occured. Once the game is started, the <c>GameManager</c> determines
        /// the offensive and defensive player for the current game and then notifies 
        /// the player that they need to take their turn.
        /// </summary>
        public void StartGame()
        {
            GameState gameState = GetCurrentGame();

            // Must initialize game first
            if (gameState == null || gameState.Status != GameStatus.Initialized)
            {
                return;
            }
            gameState.Status = GameStatus.Started;
            gameState.CurrentPlayer.Play(this, GetGameSeries(), gameState);
        }

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
        public void MakePlay(PlayerMove playerMove)
        {
            GameState gameState = GetCurrentGame();

            // Ensure game is started
            if (gameState.Status != GameStatus.Started)
            {
                return;
            }

            // Claim cell
            if (!Claim(gameState, playerMove))
            {
                return;
            }

            // Check for win
            if (Win(gameState, playerMove))
            {
                return;
            }

            // Check for tie
            if (Tie(gameState, playerMove))
            {
                return;
            }

            // Switch to player 2
            if (gameState.CurrentPlayer == GetGameSeries().Player1)
            {
                gameState.CurrentPlayer = GetGameSeries().Player2;
            }
            // Switch to player 1
            else
            {
                gameState.CurrentPlayer = GetGameSeries().Player1;
            }

            // Next player's turn
            gameState.CurrentPlayer.Play(this, GetGameSeries(), gameState);
        }

        /// <summary>
        /// Method <c>Claim</c> is called after the <c>GameManager</c> determines
        /// that the game board cell is open and the user can take ownership of
        /// the cell.
        /// </summary>
        /// <c>GameState</c> is the instance of the current game in play or last 
        /// completed.
        /// </returns>
        /// <param name="playerMove">
        /// <c>playerMove</c> is the combination of the play and game cell coordinates 
        /// associated with a players turn. Using player moves we can track the path of
        /// how users / opponents play.
        /// </param>
        /// <returns></returns>
        private bool Claim( GameState gameState, PlayerMove playerMove )
        {
            GameBoardCell gameBoardCell = gameState.GameBoard.Cells[playerMove.Row, playerMove.Column];

            // Ensure that selected cell has not been claimed
            if (gameBoardCell.Claim != null)
            {
                return false;
            }

            //  Make sure the player who made the move is the current player
            if (gameState.CurrentPlayer != playerMove.Player)
            {
                return false;
            }

            // Claim the cell
            gameBoardCell.Claim = playerMove.Player;

            // Append player move to game state
            gameState.PlayerMoves.Add(playerMove);

            // Notify listeners that the cell has been claimed
            _eventBus.PlayerClaimEvents.Notify(new PlayerClaimEvent(playerMove.Player, playerMove));

            return true;
        }

        /// <summary>
        /// Method <c>Win</c> is called after the <c>GameManager</c> determines
        /// that the game board cell is open, the user can take ownership of the cell,
        /// and the player move won the game.
        /// </summary>
        /// <c>GameState</c> is the instance of the current game in play or last 
        /// completed.
        /// </returns>
        /// <param name="playerMove">
        /// <c>playerMove</c> is the combination of the play and game cell coordinates 
        /// associated with a players turn. Using player moves we can track the path of
        /// how users / opponents play.
        /// </param>
        /// <returns></returns>
        private bool Win( GameState gameState, PlayerMove playerMove )
        {
            List<GameBoardCell> winCells = _playerPathTriangulation
                .FindPath(gameState.GameBoard.Cells, playerMove, gameState.LengthToWin);

            if (winCells != null)
            {
                // Mark game completed
                gameState.Status = GameStatus.Completed;

                // Mark winning player
                gameState.WinningPlayer = playerMove.Player;

                // Notify listeners that the player won
                _eventBus.PlayerWinEvents.Notify(new PlayerWinEvent(playerMove.Player, winCells));

                return true;
            }
            return false;
        }

        /// <summary>
        /// Method <c>Tie</c> is called after the <c>GameManager</c> determines that
        /// no more empty board cells remain which results in a final tied game outcome.
        /// </summary>
        /// <c>GameState</c> is the instance of the current game in play or last 
        /// completed.
        /// </returns>
        /// <param name="playerMove">
        /// <c>playerMove</c> is the combination of the play and game cell coordinates 
        /// associated with a players turn. Using player moves we can track the path of
        /// how users / opponents play.
        /// </param>
        /// <returns></returns>
        private bool Tie( GameState gameState, PlayerMove playerMove )
        {
            if (gameState.PlayerMoves.Count == gameState.GameBoard.Cells.Length)
            {
                // Mark game completed
                gameState.Status = GameStatus.Completed;

                // Notify listeners that there was a tie
                _eventBus.TieGameEvents.Notify(new GameEvent());

                return true;
            }
            return false;
        }

        /// <summary>
        /// Method <c>OnBoardClick</c> represents a listener callback to a <c>BoardClickEvent</c>.
        /// When a human player clicks the board, the game board notifies the <c>GameManager</c>
        /// that a cell was clicked. Event though the user might be able to click on the cell,
        /// it might not be the player's move or the player might have clicked on a claimed cell. 
        /// </summary>
        /// <param name="eventType">
        /// <c>evenType</c> represents the <c>BaordClickEvent</c> that occured and through it
        /// you can pull out the selected row and column.
        /// </param>
        private void OnBoardClick( BoardClickEvent eventType )
        {
            GameState gameState = GetCurrentGame();

            // Game completed, start new game
            if (gameState.Status == GameStatus.Completed)
            {
                _eventBus.NewGameEvents.Notify(new GameEvent());
                return;
            }

            // Current player is the ai
            if (gameState.CurrentPlayer is IComputerPlayer)
            {
                return;
            }

            MakePlay(new PlayerMove(gameState.CurrentPlayer, eventType.Row, eventType.Column));
        }
    }
}
