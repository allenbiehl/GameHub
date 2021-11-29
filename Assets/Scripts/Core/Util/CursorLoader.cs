using UnityEngine;

namespace GameHub.Core.Util
{
    public static class CursorLoader
    {
        public static Texture2D Load(CursorType cursor)
        {
            return UnityEngine.Resources.Load($"Cursors/{cursor.ToString()}", typeof(Texture2D)) as Texture2D;
        }
    }
}