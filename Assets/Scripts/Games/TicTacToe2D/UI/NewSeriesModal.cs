using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameHub.Core.Security;
using GameHub.Games.TicTacToe2D.AI;
using GameHub.Games.TicTacToe2D.Event;
using Zenject;

namespace GameHub.Games.TicTacToe2D.UI
{
    /// <summary>
    /// class <c>NewSeriesModal</c> represents the modal for starting a new Tic Tac Toe
    /// game series with an opponent. 
    /// </summary>
    public class NewSeriesModal : MonoBehaviour
    {
        /// <summary>
        /// Instance variable <c>_userInfoService</c> is responsible for managing
        /// the current user's <c>UserInfo</c>.
        /// </summary>
        private IUserInfoService _userInfoService;

        /// <summary>
        /// Instance variable <c>_playerSettingsService</c> is used to retrieve and persist 
        /// player game settings across game sessions.
        /// </summary>
        private IPlayerSettingsService _playerSettingsService;

        /// <summary>
        /// Instance variable <c>_opponentDropdown</c> is used to store the opponent dropdown.
        /// </summary>
        [SerializeField]
        private Dropdown _opponentDropdown;

        /// <summary>
        /// Instance variable <c>_winLengthDropdown</c> is used to store the total number of
        /// sequential pieces needed to win dropdown.
        /// </summary>
        [SerializeField]
        private Dropdown _winLengthDropdown;

        /// <summary>
        /// Instance variable <c>_boardSizeDropdown</c> is used to store the size of the board
        /// dropdown.
        /// </summary>
        [SerializeField]
        private Dropdown _boardSizeDropdown;

        /// <summary>
        /// Instance variable <c>_startButton</c> is used to store the start game button.
        /// </summary>
        [SerializeField]
        private Button _startButton;

        /// <summary>
        /// Instance variable <c>_startButton</c> is used to store the cancel game button.
        /// </summary>
        [SerializeField]
        private Button _cancelButton;

        /// <summary>
        /// Method <c>Setup</c> is responsible for wiring up depedencies on object creation.
        /// </summary>
        /// <param name="userInfoService">
        /// <c>userInfoService</c> is reponsible for managing the current user's <c>UserInfo</c>
        /// </param>
        /// <param name="playerSettingsService">
        /// <c>playerSettingsService</c> is used to retrieve and persist player game settings.
        /// </param>
        [Inject]
        public void Setup(IUserInfoService userInfoService, IPlayerSettingsService playerSettingsService)
        {
            _userInfoService = userInfoService;
            _playerSettingsService = playerSettingsService;
        }

        /// <summary>
        /// Method <c>Start</c> handles all initialization routines.
        /// </summary>
        private void Start()
        {
            Close();

            if (_opponentDropdown)
            {
                _opponentDropdown.ClearOptions();
                List<Dropdown.OptionData> opponentOptions = new List<Dropdown.OptionData>
                {
                    new Dropdown.OptionData(Opponent.AIExpert),
                    new Dropdown.OptionData(Opponent.AIBeginner),
                    new Dropdown.OptionData(Opponent.MultiPlayer)
                };
                _opponentDropdown.options = opponentOptions;
            }

            if (_winLengthDropdown)
            {
                _winLengthDropdown.ClearOptions();
                List<Dropdown.OptionData> winLengthOptions = new List<Dropdown.OptionData>
                {
                    new Dropdown.OptionData("3"),
                    new Dropdown.OptionData("4"),
                    new Dropdown.OptionData("5"),
                    new Dropdown.OptionData("6")
                };
                _winLengthDropdown.options = winLengthOptions;
            }

            if (_boardSizeDropdown)
            {
                _boardSizeDropdown.ClearOptions();
                List<Dropdown.OptionData> boardSizeOptions = new List<Dropdown.OptionData>
                {
                    new Dropdown.OptionData("3"),
                    new Dropdown.OptionData("4"),
                    new Dropdown.OptionData("5"),
                    new Dropdown.OptionData("6"),
                    new Dropdown.OptionData("7"),
                    new Dropdown.OptionData("8"),
                    new Dropdown.OptionData("9"),
                    new Dropdown.OptionData("10"),
                    new Dropdown.OptionData("11"),
                    new Dropdown.OptionData("12"),
                    new Dropdown.OptionData("13"),
                    new Dropdown.OptionData("14"),
                    new Dropdown.OptionData("15"),
                    new Dropdown.OptionData("16"),
                    new Dropdown.OptionData("17"),
                    new Dropdown.OptionData("18"),
                    new Dropdown.OptionData("19"),
                    new Dropdown.OptionData("20")
                };
                _boardSizeDropdown.options = boardSizeOptions;
            }
        }

        /// <summary>
        /// Method <c>Open</c> is used to open / show the modal to the user.
        /// </summary>
        /// <param name="onSave">
        public void Open()
        {
            SetActive(true);

            if (_startButton)
            {
                _startButton.onClick.RemoveAllListeners();
                _startButton.onClick.AddListener(() => {
                    SaveSettings();
                    StartSeries();
                });
                _startButton.onClick.AddListener(Close);
            }
            if (_cancelButton)
            {
                _cancelButton.onClick.RemoveAllListeners();
                _cancelButton.onClick.AddListener(Close);
            }

            PlayerSettings playerSettings = _playerSettingsService.GetSettings();

            if (_opponentDropdown)
            {
                _opponentDropdown.value = _opponentDropdown.options
                .FindIndex(option => option.text == playerSettings.Opponent);
            }
            if (_winLengthDropdown)
            {
                _winLengthDropdown.value = _winLengthDropdown.options
                .FindIndex(option => option.text == playerSettings.LengthToWin.ToString());
            }
            if (_boardSizeDropdown)
            {
                _boardSizeDropdown.value = _boardSizeDropdown.options
                .FindIndex(option => option.text == playerSettings.BoardSize.ToString());
            }
        }

        /// <summary>
        /// Method <c>Close</c> is used to close / hide the modal from the user.
        /// </summary>
        private void Close()
        {
            SetActive(false);
        }

        /// <summary>
        /// Method <c>SetActive</c> is used to show or hide the modal.
        /// </summary>
        /// <param name="active">
        /// <c>active</c> represent whether the modal should be visible (true)
        /// or hidden (false).
        /// </param>
        private void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        /// <summary>
        /// Method <c>SaveSettings</c> is used to save <c>PlayerSettings</c> via the
        /// <c>PlayerSettingsManager</c>.
        /// </summary>
        private void SaveSettings()
        {
            PlayerSettings settings = _playerSettingsService.GetSettings();

            if (_opponentDropdown)
            {
                settings.Opponent = _opponentDropdown.options[_opponentDropdown.value].text;
            }
            if (_winLengthDropdown)
            {
                settings.LengthToWin = Int32.Parse(_winLengthDropdown.options[_winLengthDropdown.value].text);
            }
            if (_boardSizeDropdown)
            {
                settings.BoardSize = Int32.Parse(_boardSizeDropdown.options[_boardSizeDropdown.value].text);
            }
            _playerSettingsService.SaveSettings(settings);
        }

        /// <summary>
        /// Method <c>StartSeries</c> is used to determine the next player in the series, 
        /// start a new series via the game manager and then notify all new series event 
        /// listeners that a new series was started.
        /// </summary>
        private void StartSeries()
        {
            PlayerSettings settings = _playerSettingsService.GetSettings();
            UserInfo player1UserInfo = _userInfoService.GetUserInfo();
            IPlayer player1 = new HumanPlayer(player1UserInfo, settings);

            IPlayer player2;
            
            if (settings.Opponent == Opponent.AIExpert)
            {
                player2 = ExpertComputerPlayer.Default;
            }
            else if (settings.Opponent == Opponent.AIBeginner)
            {
                player2 = BeginnerComputerPlayer.Default;
            }
            else
            {
                player2 = new HumanPlayer(player1UserInfo, PlayerSettings.Omega);
            }

            // Create new series
            GameManager.Instance.StartSeries(player1, player2);

            // Game is ready to start
            GameManager.Instance.EventBus.NewSeriesEvents.Notify(new GameEvent());
        }
    }
}
