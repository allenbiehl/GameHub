using UnityEngine;
using UnityEngine.UI;
using GameHub.Core.Util;

namespace GameHub.Games.TicTacToe2D.UI
{
    public class SettingsButton : MonoBehaviour
    {
        void Start()
        {
            Button button = ComponentUtil.GetComponent<Button>(this);

            if (button)
            {
                button.onClick.AddListener(OpenSettings);
            }
        }

        private void OpenSettings()
        {
            
        }
    }
}