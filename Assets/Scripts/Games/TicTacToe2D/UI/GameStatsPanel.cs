
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using GameHub.Core.Util;
using GameHub.Games.TicTacToe2D.Event;
using TMPro;

namespace GameHub.Games.TicTacToe2D.UI
{
    public class GameStatsPanel : MonoBehaviour
    {
        private Image _player1AvatarIcon;
        private TMP_Text _player1AvatarText;
        private TMP_Text _player1Score;

        private Image _player2AvatarIcon;
        private TMP_Text _player2AvatarText;
        private TMP_Text _player2Score;

        private TMP_Text _tieScore;

        private void Start()
        {
            GameManager.Instance.EventBus.NewSeriesEvents.AddListener(OnNewSeries);
            GameManager.Instance.EventBus.TieGameEvents.AddListener(OnTieGame);
            GameManager.Instance.EventBus.PlayerWinEvents.AddListener(OnPlayerWin);

            _player1AvatarIcon = ComponentUtil.FindComponent<Image>("Player1Stats/Avatar");
            _player1AvatarText = ComponentUtil.FindComponent<TMP_Text>("Player1Stats/Avatar/Text");
            _player1Score = ComponentUtil.FindComponent<TMP_Text>("Player1Stats/Score");

            _player2AvatarIcon = ComponentUtil.FindComponent<Image>("Player2Stats/Avatar");
            _player2AvatarText = ComponentUtil.FindComponent<TMP_Text>("Player2Stats/Avatar/Text");
            _player2Score = ComponentUtil.FindComponent<TMP_Text>("Player2Stats/Score");

            _tieScore = ComponentUtil.FindComponent<TMP_Text>("TieStats/Score");

            this.gameObject.SetActive(false);
        }

        private void OnTieGame(GameEvent eventType)
        {
            int score = Int32.Parse(_tieScore.text) + 1;
            _tieScore.text = $"{score}";
        }

        private void OnPlayerWin(PlayerWinEvent eventType)
        {
            if (eventType.Player == GameManager.Instance.GameSeries.Player1)
            {
                int score = Int32.Parse(_player1Score.text) + 1;
                _player1Score.text = $"{score}";
            }
            else
            {
                int score = Int32.Parse(_player2Score.text) + 1;
                _player2Score.text = $"{score}";
            }
        }

        private void OnNewSeries(GameEvent eventType)
        {
            GameSeries series = GameManager.Instance.GameSeries;

            if (series != null)
            {
                this.gameObject.SetActive(true);

                _player1AvatarIcon.color = series.Player1.Settings.Color;
                _player1AvatarText.text = series.Player1.UserInfo.Initials;
                _player1Score.text = "0";

                _player2AvatarIcon.color = series.Player2.Settings.Color;
                _player2AvatarText.text = series.Player2.UserInfo.Initials;
                _player2Score.text = "0";

                _tieScore.text = "0";
            }
        }
    }
}