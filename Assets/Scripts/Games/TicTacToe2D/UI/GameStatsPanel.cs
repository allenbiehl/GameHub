using System;
using UnityEngine;
using UnityEngine.UI;
using GameHub.Games.TicTacToe2D.Event;
using TMPro;
using Zenject;

namespace GameHub.Games.TicTacToe2D.UI
{
    /// <summary>
    /// Class <c>GameStatePanel</c> represents the UI panel responsible for rendering
    /// game session stats which includes player colors, initials, and scores.
    /// </summary>
    public class GameStatsPanel : MonoBehaviour
    {
        /// <summary>
        /// Instance variable <c>_gameManager</c> is used to control all game moves and 
        /// game state.
        /// </summary>
        private IGameManager _gameManager;

        /// <summary>
        /// Instance variable <c>_player1AvatarIcon</c> is the player 1 icon and is
        /// used to color coded according to the player 1 color scheme, which includes
        /// the avatar color and the game piece color.
        /// </summary>
        [SerializeField]
        private Image _player1AvatarIcon;

        /// <summary>
        /// Instance variable <c>_player1AvatarText</c> is the player 1 intials and is
        /// used to uniquely identify the player 1 using their initials.
        /// </summary>
        [SerializeField]
        private TMP_Text _player1AvatarText;

        /// <summary>
        /// Instance variable <c>_player1Score</c> is the player 1 score that represents 
        /// the cumulative wins for player 1 for the current game series.
        /// </summary>
        [SerializeField]
        private TMP_Text _player1Score;

        /// <summary>
        /// Instance variable <c>_player2AvatarIcon</c> is the player 2 icon and is
        /// used to color coded according to the player 2 color scheme, which includes
        /// the avatar color and the game piece color.
        /// </summary>
        [SerializeField]
        private Image _player2AvatarIcon;

        /// <summary>
        /// Instance variable <c>_player2AvatarText</c> is the player 2 intials and is
        /// used to uniquely identify the player 2 using their initials.
        /// </summary>
        [SerializeField]
        private TMP_Text _player2AvatarText;

        /// <summary>
        /// Instance variable <c>_player2Score</c> is the player 2 score that represents 
        /// the cumulative wins for player 2 for the current game series.
        /// </summary>
        [SerializeField]
        private TMP_Text _player2Score;

        /// <summary>
        /// Instance variable <c>_tieScore</c> is the cumulative number of tied games
        /// between player 1 and player 2 for the current game series.
        /// </summary>
        [SerializeField]
        private TMP_Text _tieScore;

        /// <summary>
        /// Method <c>Setup</c> is responsible for wiring up depedencies on object creation.
        /// </summary>
        /// <param name="gameManager">
        /// <c>gameManager</c> is used to control all game moves and game state.
        /// </param>
        [Inject]
        public void Setup(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        /// <summary>
        /// Method <c>Start</c> is used to initialze the component.
        /// </summary>
        private void Start()
        {
            _gameManager.GetEventBus().NewSeriesEvents.AddListener(OnNewSeries);
            _gameManager.GetEventBus().TieGameEvents.AddListener(OnTieGame);
            _gameManager.GetEventBus().PlayerWinEvents.AddListener(OnPlayerWin);

            this.gameObject.SetActive(false);
        }

        /// <summary>
        /// Method <c>OnTieGame</c> is executed when the current game completes,
        /// no player 1, and so the tied score is incremented for the current game series.
        /// </summary>
        /// <param name="eventType">
        /// <c>eventType</c> is a generic event with no additional information.
        /// </param>
        private void OnTieGame(GameEvent eventType)
        {
            int score = Int32.Parse(_tieScore.text) + 1;
            SetTieScore(score);
        }

        /// <summary>
        /// Method <c>OnPlayerWin</c> is executed when the current game completes,
        /// and there is a winner and the score is incremented for the winning player.
        /// </summary>
        /// <param name="eventType">
        /// <c>eventType</c> is event associated with the winning player and is used
        /// to determine which player's score to increment.
        /// </param>
        private void OnPlayerWin(PlayerWinEvent eventType)
        {
            if (eventType.Player == _gameManager.GetGameSeries().Player1)
            {
                int score = Int32.Parse(_player1Score.text) + 1;
                SetPlayer1Score(score);
            }
            else
            {
                int score = Int32.Parse(_player2Score.text) + 1;
                SetPlayer2Score(score);
            }
        }

        /// <summary>
        /// Method <c>OnNewSeries</c> is executed when a new game series starts and
        /// the player stats need to be reset back to 0 and associated with the new
        /// players. 
        /// </summary>
        /// <param name="eventType">
        /// <c>eventType</c> is a generic event with no additional information.
        /// </param>
        private void OnNewSeries(GameEvent eventType)
        {
            GameSeries series = _gameManager.GetGameSeries();

            if (series != null)
            {
                this.gameObject.SetActive(true);

                SetPlayer1Color(series.Player1.Settings.Color);
                SetPlayer1Initials(series.Player1.UserInfo.Initials);
                SetPlayer1Score(0);

                SetPlayer2Color(series.Player2.Settings.Color);
                SetPlayer2Initials(series.Player2.UserInfo.Initials);
                SetPlayer2Score(0);

                SetTieScore(0);
            }
        }

        /// <summary>
        /// Method <c>SetPlayer1Color</c> is used to update the player 1 avatar 
        /// background color.
        /// </summary>
        /// <param name="value">
        /// <c>value</c> is player 1 avatar background color to display
        /// </param>
        private void SetPlayer1Color(Color value)
        {
            if (_player1AvatarIcon)
            {
                _player1AvatarIcon.color = value;
            }
        }

        /// <summary>
        /// Method <c>SetPlayer1Initials</c> is used to update the player 1 avatar 
        /// initials.
        /// </summary>
        /// <param name="value">
        /// <c>value</c> is player 1 avatar intials to display
        /// </param>
        private void SetPlayer1Initials(string value)
        {
            if (_player1AvatarText)
            {
                _player1AvatarText.text = value;
            }
        }

        /// <summary>
        /// Method <c>SetPlayer1Score</c> is used to update the player 1 total
        /// for the current series.
        /// </summary>
        /// <param name="value">
        /// <c>value</c> is player 1 total score
        /// </param>
        private void SetPlayer1Score(int value)
        {
            if (_player1Score)
            {
                _player1Score.text = value.ToString();
            }
        }

        /// <summary>
        /// Method <c>SetPlayer2Color</c> is used to update the player 2 avatar 
        /// background color.
        /// </summary>
        /// <param name="value">
        /// <c>value</c> is player 2 avatar background color to display
        /// </param>
        private void SetPlayer2Color(Color value)
        {
            if (_player2AvatarIcon)
            {
                _player2AvatarIcon.color = value;
            }
        }

        /// <summary>
        /// Method <c>SetPlayer2Initials</c> is used to update the player 2 avatar 
        /// initials.
        /// </summary>
        /// <param name="value">
        /// <c>value</c> is player 2 avatar intials to display
        /// </param>
        private void SetPlayer2Initials(string value)
        {
            if (_player2AvatarText)
            {
                _player2AvatarText.text = value;
            }
        }

        /// <summary>
        /// Method <c>SetPlayer2Score</c> is used to update the player 2 total
        /// for the current series.
        /// </summary>
        /// <param name="value">
        /// <c>value</c> is player 2 total score
        /// </param>
        private void SetPlayer2Score(int value)
        {
            if (_player2Score)
            {
                _player2Score.text = value.ToString();
            }
        }

        /// <summary>
        /// Method <c>SetTieScore</c> is the total number of tied games between 
        /// player 1 and player 2 for the current game series.
        /// </summary>
        /// <param name="value">
        /// <c>value</c> is total number of tied games
        /// </param>
        private void SetTieScore(int value)
        {
            if (_tieScore)
            {
                _tieScore.text = value.ToString();
            }
        }
    }
}