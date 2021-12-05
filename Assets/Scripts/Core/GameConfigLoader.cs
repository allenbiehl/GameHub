using System;
using System.Collections.Generic;
using UnityEngine;
using GameHub.Core.Util;

namespace GameHub.Core
{
    /// <summary>
    /// Class <c>GameConfigLoader</c> provides the ability to retrieve a list of 
    /// all available game configurations that can be played.
    /// </summary>
    public class GameConfigLoader
    {
        /// <summary>
        /// Instance variable <c>_instance</c> stores the <c>GameConfigLoader</c>
        /// singleton instance.
        /// </summary>
        private static readonly Lazy<GameConfigLoader> _instance =
            new Lazy<GameConfigLoader>(() => new GameConfigLoader());

        /// <summary>
        /// Method <c>Instance</c> returns the <c>GameConfigLoader</c> singleton 
        /// instance.
        /// </summary>
        public static GameConfigLoader Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        /// <summary>
        /// Private constructor to ensure the <c>GameConfigLoader</c> cannot 
        /// be instantiated externally.
        /// </summary>
        private GameConfigLoader()
        {
        }

        /// <summary>
        /// Method <c>Load</c> is used to retrieve a list of all available game 
        /// configurations that can be played.
        /// </summary>
        /// <returns></returns>
        public List<GameConfig> Load()
        {
            Sprite sprite;
            List<GameConfig> gameConfigs = new List<GameConfig>();

            sprite = SpriteLoader.Instance.Load("grid");
            gameConfigs.Add(new GameConfig("Tic Tac Toe 2D", "TicTacToe2D", sprite));

            //sprite = SpriteLoader.Load("rubik");
            //gameConfigs.Add(new GameConfig("Tic Tac Toe 3D", "TicTacToe3D", sprite));

            return gameConfigs;
        }
    }
}