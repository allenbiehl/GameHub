using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameHub.Core.Security;
using GameHub.Games.TicTacToe2D.AI;
using GameHub.Games.TicTacToe2D.Event;

namespace GameHub.Games.TicTacToe2D.UI
{
    public class NewSeriesModal : MonoBehaviour
    {
        private static NewSeriesModal _instance;

        public Dropdown opponentDropdown;
        public Dropdown winLengthDropdown;
        public Dropdown boardSizeDropdown;

        public Button startButton;
        public Button cancelButton;

        public static NewSeriesModal Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = FindObjectOfType(typeof(NewSeriesModal)) as NewSeriesModal;
                }
                return _instance;
            }
        }

        private void Start()
        {
            Instance.Close();

            opponentDropdown.ClearOptions();
            List<Dropdown.OptionData> opponentOptions = new List<Dropdown.OptionData>
            {
                new Dropdown.OptionData(Opponent.AIExpert),
                new Dropdown.OptionData(Opponent.AIBeginner),
                new Dropdown.OptionData(Opponent.MultiPlayer)
            };
            opponentDropdown.options = opponentOptions;

            winLengthDropdown.ClearOptions();
            List<Dropdown.OptionData> winLengthOptions = new List<Dropdown.OptionData>
            {
                new Dropdown.OptionData("3"),
                new Dropdown.OptionData("4"),
                new Dropdown.OptionData("5"),
                new Dropdown.OptionData("6")
            };
            winLengthDropdown.options = winLengthOptions;

            boardSizeDropdown.ClearOptions();
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
            boardSizeDropdown.options = boardSizeOptions;
        }

        public void Open()
        {
            SetActive(true);

            startButton.onClick.RemoveAllListeners();
            startButton.onClick.AddListener(() => {
                SaveSettings();
                StartSeries();
            });
            startButton.onClick.AddListener(Close);

            cancelButton.onClick.RemoveAllListeners();
            cancelButton.onClick.AddListener(Close);

            PlayerSettings playerSettings = PlayerSettingsManager.GetSettings();

            opponentDropdown.value = opponentDropdown.options
                .FindIndex(option => option.text == playerSettings.Opponent);

            winLengthDropdown.value = winLengthDropdown.options
                .FindIndex(option => option.text == playerSettings.LengthToWin.ToString());

            boardSizeDropdown.value = boardSizeDropdown.options
                .FindIndex(option => option.text == playerSettings.BoardSize.ToString());
        }

        void Close()
        {
            SetActive(false);
        }

        void SetActive(bool active)
        {
            Instance.gameObject.SetActive(active);
        }

        void SaveSettings()
        {
            PlayerSettings settings = PlayerSettingsManager.GetSettings();
            settings.Opponent = opponentDropdown.options[opponentDropdown.value].text;
            settings.LengthToWin = Int32.Parse(winLengthDropdown.options[winLengthDropdown.value].text);
            settings.BoardSize = Int32.Parse(boardSizeDropdown.options[boardSizeDropdown.value].text);
            PlayerSettingsManager.SaveSettings(settings);
        }

        void StartSeries()
        {
            PlayerSettings settings = PlayerSettingsManager.GetSettings();

            IPlayer player1 = new HumanPlayer(
                UserInfoManager.GetUserInfo(),
                PlayerSettingsManager.GetSettings()
            );

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
                player2 = new HumanPlayer(
                    UserInfoManager.GetUserInfo(),
                    PlayerSettings.Omega
                );
            }

            // Create new series
            GameManager.Instance.StartSeries(player1, player2);

            // Game is ready to start
            GameManager.Instance.EventBus.NewSeriesEvents.Notify(new GameEvent());
        }
    }
}
