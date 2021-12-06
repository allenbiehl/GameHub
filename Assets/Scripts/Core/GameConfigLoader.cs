using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GameHub.Core
{
    /// <summary>
    /// Class <c>GameConfigService</c> provides the ability to retrieve a list of 
    /// all available game configurations that can be played.
    /// </summary>
    public class GameConfigLoader : IGameConfigLoader
    {
        /// <summary>
        /// Instance variable <c>_resourceLoader</c> is used load system resources.s
        /// </summary>
        private IResourceLoader _resourceLoader;

        /// <summary>
        /// Constructor for <c>GameConfigLoader</c>
        /// </summary>
        /// <param name="gameSettingsService">
        /// <c>resourceLoader</c> is used to load system resources.
        /// </param>
        [Inject]
        public GameConfigLoader(IResourceLoader resourceLoader)
        {
            _resourceLoader = resourceLoader;
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

            sprite = _resourceLoader.LoadSprite("grid");
            gameConfigs.Add(new GameConfig("Tic Tac Toe 2D", "TicTacToe2D", sprite));

            //sprite = SpriteLoader.Load("rubik");
            //gameConfigs.Add(new GameConfig("Tic Tac Toe 3D", "TicTacToe3D", sprite));

            return gameConfigs;
        }
    }
}