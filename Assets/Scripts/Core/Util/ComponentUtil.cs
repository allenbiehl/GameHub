using UnityEngine;

namespace GameHub.Core.Util
{
    public static class ComponentUtil
    {
        public static T GetComponent<T>( UnityEngine.MonoBehaviour context )
        {
            return context.GetComponent<T>();
        }

        public static T FindComponent<T>( string path )
        {
            return GameObject.Find(path).GetComponent<T>();
        }

        public static T FindComponent<T>( string path, UnityEngine.MonoBehaviour context )
        {
            return FindComponent<T>( path, context.transform );
        }

        public static T FindComponent<T>( string path, Transform context )
        {
            Transform transform = context.transform.Find(path);
            return transform ? transform.GetComponent<T>() : default(T);
        }
    }
}
