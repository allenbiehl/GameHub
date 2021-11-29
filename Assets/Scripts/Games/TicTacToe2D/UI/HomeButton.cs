using UnityEngine;
using UnityEngine.UI;
using GameHub.Core;
using GameHub.Core.Util;

namespace GameHub.Games.TicTacToe2D.UI
{
    public class HomeButton : MonoBehaviour
    {
        void Start()
        {
            Button button = ComponentUtil.GetComponent<Button>(this);

            if (button)
            {
                button.onClick.AddListener(() => SceneLoader.LoadMainMenu());
            }
        }
    }
}