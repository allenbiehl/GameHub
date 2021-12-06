using System;
using UnityEngine;

namespace GameHub.Core
{
    /// <summary>
    /// Class <c>ResourceLoader</c> is a utility for loading sprite resources stored
    /// in the resources/sprite directory.
    /// </summary>
    public class ResourceLoader : IResourceLoader
    {
        /// <summary>
        /// Method <c>LoadSprite</c> is used for loading a sprite based on the unique 
        /// sprite name.
        /// </summary>
        /// <param name="sprite">
        /// <c>sprite</c> is the unique sprite name store in resources/sprites.
        /// </param>
        /// <returns>
        /// <c>Sprite</c> instance associated with unique sprite name.
        /// </returns>
        public Sprite LoadSprite(string sprite)
        {
            return UnityEngine.Resources.Load($"Sprites/{sprite}", typeof(Sprite)) as Sprite;
        }
    }
}