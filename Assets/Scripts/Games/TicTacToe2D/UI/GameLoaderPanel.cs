using UnityEngine;

namespace GameHub.Games.TicTacToe2D.UI
{
    public class GameLoaderPanel : MonoBehaviour
    {
        void Awake()
        {
            GameManager.Initialize();
        }
    }
}
