using System.Collections.Generic;
using UnityEngine;

namespace GameHub.Core
{
    /// <summary>
    /// Interface <c>IGameConfigLoader</c> represents the base interface 
    /// for all <c>GameConfigLoader</c> implementations.
    /// </summary>
    public interface IGameConfigLoader
    {
        /// <summary>
        /// Method <c>Load</c> is used to retrieve a list of all available game 
        /// configurations that can be played.
        /// </summary>
        /// <returns></returns>
        List<GameConfig> Load();
    }
}
