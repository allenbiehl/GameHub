using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameHub.Games.TicTacToe2D.UI
{
    /// <summary>
    /// class <c>NewSeriesModal</c> represents the modal for starting a new Tic Tac Toe
    /// game series with an opponent. 
    /// </summary>
    public class PlayAgainModal : MonoBehaviour
    {
        /// <summary>
        /// Instance variable <c>_instance</c> for storing the <c>PlayAgainModal</c>
        /// singleton instance.
        /// </summary>
        private static PlayAgainModal _instance;

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
        /// Instance variable <c>_yesButton</c> is used to store the yes button.
        /// </summary>
        [SerializeField]
        private Button _yesButton;

        /// <summary>
        /// Instance variable <c>_noButton</c> is used to store the no button.
        /// </summary>
        [SerializeField]
        private Button _noButton;

        /// <summary>
        /// Property <c>Instance</c> returns the <c>PlayAgainModal</c> singleton 
        /// instance.
        /// </summary>
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

        /// <summary>
        /// Method <c>Start</c> handles all initialization routines.
        /// </summary>
        private void Start()
        {
            Instance.Close();

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
        /// <param name="onYes">
        /// <c>onYes</c> represents the callback that will be called when the 
        /// user clicks the Yes button. 
        /// </param>
        /// <param name="onYes">
        /// <c>onNo</c> represents the callback that will be called when the 
        /// user clicks the No button. 
        /// </param>
        public void Open(UnityAction onYes, UnityAction onNo)
        {
            SetActive(true);

            if (_yesButton)
            {
                _yesButton.onClick.RemoveAllListeners();
                _yesButton.onClick.AddListener(() => {
                    SaveSettings(onYes);
                });
                _yesButton.onClick.AddListener(Close);
            }

            if (_noButton)
            {
                _noButton.onClick.RemoveAllListeners();
                _noButton.onClick.AddListener(Close);
                _noButton.onClick.AddListener(onNo);
            }

            PlayerSettings playerSettings = PlayerSettingsManager.Instance.GetSettings();

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
        void Close()
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
        void SetActive(bool active)
        {
            _instance.gameObject.SetActive(active);
        }

        /// <summary>
        /// Method <c>SaveSettings</c> is used to save <c>UserInfo</c> via the
        /// <c>PlayerSettingsManager</c> and then execute the associated <c>onSave</c>
        /// callback.
        /// </summary>
        /// <param name="onSave">
        /// <c>onSave</c> callback that is invoked after settings are successfully saved.
        /// </param>
        void SaveSettings(UnityAction onSave)
        {
            PlayerSettings settings = PlayerSettingsManager.Instance.GetSettings();

            if (_winLengthDropdown)
            {
                settings.LengthToWin = Int32.Parse(_winLengthDropdown.options[_winLengthDropdown.value].text);
            }
            if (_boardSizeDropdown)
            {
                settings.BoardSize = Int32.Parse(_boardSizeDropdown.options[_boardSizeDropdown.value].text);
            }
            PlayerSettingsManager.Instance.SaveSettings(settings);

            onSave.Invoke();
        }
    }
}
