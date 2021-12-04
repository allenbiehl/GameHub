using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameHub.Games.TicTacToe2D.UI
{
    public class PlayAgainModal : MonoBehaviour
    {
        private static PlayAgainModal _instance;

        public Dropdown winLengthDropdown;
        public Dropdown boardSizeDropdown;

        public Button yesButton;
        public Button noButton;

        private int _winLength;
        private int _boardSize;

        public static PlayAgainModal Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = FindObjectOfType(typeof(PlayAgainModal)) as PlayAgainModal;
                }
                return _instance;
            }
        }

        private void Start()
        {
            Instance.Close();

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

        public void Open(UnityAction onYes)
        {
            SetActive(true);

            yesButton.onClick.RemoveAllListeners();
            yesButton.onClick.AddListener(() => {
                SaveSettings(onYes);
            });
            yesButton.onClick.AddListener(Close);

            noButton.onClick.RemoveAllListeners();
            noButton.onClick.AddListener(Close);

            PlayerSettings playerSettings = PlayerSettingsManager.Instance.GetSettings();

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
            _instance.gameObject.SetActive(active);
        }

        void SaveSettings(UnityAction onSave)
        {
            PlayerSettings settings = PlayerSettingsManager.Instance.GetSettings();
            settings.LengthToWin = Int32.Parse(winLengthDropdown.options[winLengthDropdown.value].text);
            settings.BoardSize = Int32.Parse(boardSizeDropdown.options[boardSizeDropdown.value].text);
            PlayerSettingsManager.Instance.SaveSettings(settings);

            onSave.Invoke();
        }
    }
}
