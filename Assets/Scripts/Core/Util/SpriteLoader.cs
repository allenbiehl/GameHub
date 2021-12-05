using System;
using UnityEngine;

namespace GameHub.Core.Util
{
    /// <summary>
    /// Class <c>SpriteLoader</c> is a utility for loading sprite resources stored
    /// in the resources/sprite directory.
    /// </summary>
    public class SpriteLoader
    {
        /// <summary>
        /// Instance variable <c>_instance</c> stores the <c>UserInfoManager</c>
        /// singleton instance.
        /// </summary>
        private static readonly Lazy<SpriteLoader> _instance =
            new Lazy<SpriteLoader>(() => new SpriteLoader());

        /// <summary>
        /// Method <c>Instance</c> returns the <c>SpriteLoader</c> singleton 
        /// instance.
        /// </summary>
        public static SpriteLoader Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        /// <summary>
        /// Private constructor to ensure the <c>SpriteLoader</c> cannot 
        /// be instantiated externally.
        /// </summary>
        private SpriteLoader()
        {
        }

        /// <summary>
        /// Method <c>Load</c> is used for loading a sprite based on the specified
        /// <c>SpriteType</c>. 
        /// </summary>
        /// <param name="sprite">
        /// <c>sprite</c> is the enum sprite type to load.
        /// </param>
        /// <returns>
        /// <c>Sprite</c> instance associated with enum sprite type.
        /// </returns>
        public Sprite Load(SpriteType sprite)
        {
            return Load(sprite.ToString());
        }

        /// <summary>
        /// Method <c>Load</c> is used for loading a sprite based on the unique 
        /// sprite name.
        /// </summary>
        /// <param name="sprite">
        /// <c>sprite</c> is the unique sprite name store in resources/sprites.
        /// </param>
        /// <returns>
        /// <c>Sprite</c> instance associated with unique sprite name.
        /// </returns>
        public Sprite Load(string sprite)
        {
            return UnityEngine.Resources.Load($"Sprites/{sprite}", typeof(Sprite)) as Sprite;
        }
    }
}