using System.Collections.Generic;
using UnityEngine;

namespace GameHub.Core
{
    /// <summary>
    /// Interface <c>IResourceLoader</c> represents the base interface 
    /// for all <c>ResourceLoader</c> implementations.
    /// </summary>
    public interface IResourceLoader
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
        public Sprite LoadSprite(string sprite);
    }
}
