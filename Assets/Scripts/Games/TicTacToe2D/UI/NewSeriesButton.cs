using UnityEngine;
using UnityEngine.UI;
using GameHub.Core.Util;
using GameHub.Core.Security;
using GameHub.Games.TicTacToe2D.Event;
using GameHub.Games.TicTacToe2D.AI;

namespace GameHub.Games.TicTacToe2D.UI
{
    public class NewSeriesButton : MonoBehaviour
    {
        void Start()
        {
            Button button = ComponentUtil.GetComponent<Button>(this);

            if (button)
            {
                button.onClick.AddListener(NewSeries);
            }
        }

        private void NewSeries()
        {
            NewSeriesModal.Instance.Open();
        }
    }
}