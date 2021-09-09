using UnityEngine;

namespace Framework.Core
{
    [DisallowMultipleComponent]
    public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T ms_instance = null;

        protected static T Instace => ms_instance;

        protected virtual void Awake()
        {
            if ( ms_instance != null )
            {
                DestroyImmediate( ms_instance.gameObject );
            }
            ms_instance = this.GetComponent<T>();
        }
    }
}
