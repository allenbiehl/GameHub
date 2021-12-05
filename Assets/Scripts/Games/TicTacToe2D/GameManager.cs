using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using GameHub.Games.TicTacToe2D.Event;
using GameHub.Games.TicTacToe2D.AI;

/// <summary>
/// 
/// </summary>
namespace GameHub.Games.TicTacToe2D
{
    public class GameManager
    {
        public GameSeries GameSeries { get; private set; }

        public EventBus EventBus { get; private set; }

        public static GameManager Instance { get; private set; }

        private PlayerPathTriangulation _playerPathTriangulation = 
            new PlayerPathTriangulation();

        private GameManager()
        {
            EventBus = new EventBus();
            EventBus.BoardClickEvents.AddListener(OnBoardClick);
        }

        public static void Initialize()
        {
            Instance = new GameManager();
        }

        public GameState CurrentGame
        {
            get
            {
                GameState game = null;

                if (GameSeries != null)
                {
                    game = GameSeries.CurrentGame;
                }
                return game;
            }
        }

        public GameSeries StartSeries(IPlayer player1, IPlayer player2)
        {
            GameSeries = new GameSeries(player1, player2);
            return GameSeries;
        }

        public GameState InitializeGame( GameBoard gameBoard, int lengthToWin )
        {
            // Must start series first
            if (GameSeries == null)
            {
                return null;
            }

            IPlayer initialPlayer;

            // No games played, Principle starts
            if (GameSeries.Games.Count == 0)
            {
                initialPlayer = GameSeries.Player1;
            }
            // Switch initial player from last game
            else
            {
                IPlayer lastPlayer = GameSeries.Games.Last().InitialPlayer;

                if (lastPlayer == GameSeries.Player1)
                {
                    initialPlayer = GameSeries.Player2;
                }
                else
                {
                    initialPlayer = GameSeries.Player1;
                }
            }

            GameState state = new GameState(gameBoard, initialPlayer, lengthToWin);
            GameSeries.Games.Add(state);
            return state;
        }

        public void StartGame()
        {
            GameState gameState = CurrentGame;

            // Must initialize game first
            if (gameState == null || gameState.Status != GameStatus.Initialized)
            {
                return;
            }
            gameState.Status = GameStatus.Started;
            gameState.CurrentPlayer.Play(this, GameSeries, gameState);
        }

        public void MakePlay( PlayerMove playerMove )
        {
            GameState gameState = CurrentGame;

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
            if (gameState.CurrentPlayer == GameSeries.Player1)
            {
                gameState.CurrentPlayer = GameSeries.Player2;
            }
            // Switch to player 1
            else
            {
                gameState.CurrentPlayer = GameSeries.Player1;
            }

            // Next player's turn
            gameState.CurrentPlayer.Play(this, GameSeries, gameState);
        }

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
            EventBus.PlayerClaimEvents.Notify(new PlayerClaimEvent(playerMove.Player, playerMove));

            return true;
        }

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
                EventBus.PlayerWinEvents.Notify(new PlayerWinEvent(playerMove.Player, winCells));

                return true;
            }
            return false;
        }

        private bool Tie( GameState gameState, PlayerMove playerMove )
        {
            if (gameState.PlayerMoves.Count == gameState.GameBoard.Cells.Length)
            {
                // Mark game completed
                gameState.Status = GameStatus.Completed;

                // Notify listeners that there was a tie
                EventBus.TieGameEvents.Notify(new GameEvent());

                return true;
            }
            return false;
        }

        private void OnBoardClick( BoardClickEvent eventType )
        {
            GameState gameState = CurrentGame;

            // Game completed, start new game
            if (gameState.Status == GameStatus.Completed)
            {
                EventBus.NewGameEvents.Notify(new GameEvent());
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
