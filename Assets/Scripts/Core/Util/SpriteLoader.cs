using UnityEngine;

namespace GameHub.Core.Util
{
    public class SpriteLoader
    {
        public static Sprite Load(SpriteType sprite)
        {
            return Load(sprite.ToString());
        }

        public static Sprite Load(string sprite)
        {
            return UnityEngine.Resources.Load($"Sprites/{sprite}", typeof(Sprite)) as Sprite;
        }
    }
}