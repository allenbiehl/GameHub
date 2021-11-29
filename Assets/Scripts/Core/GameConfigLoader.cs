using System.Collections.Generic;
using UnityEngine;
using GameHub.Core.Util;

namespace GameHub.Core
{
    public static class GameConfigLoader
    {
        public static List<GameConfig> Load()
        {
            Sprite sprite;
            List<GameConfig> gameConfigs = new List<GameConfig>();

            sprite = SpriteLoader.Load("grid");
            gameConfigs.Add(new GameConfig("Tic Tac Toe 2D", "TicTacToe2D", sprite));

            //sprite = SpriteLoader.Load("rubik");
            //gameConfigs.Add(new GameConfig("Tic Tac Toe 3D", "TicTacToe3D", sprite));

            return gameConfigs;
        }
    }
}