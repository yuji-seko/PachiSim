using System.Collections.Generic;
using UnityEngine;

namespace PachiSim.Scenes.PachiScene
{
    public class FluctuateBroadcaster : MonoBehaviour
    {
        public interface IReceiver
        {
            void OnBegin( FluctuationData data );
            void OnEnd( FluctuationData data );
        }

        //=================================================
        // Fields ( private )
        //=================================================
        [SerializeField] private List<IReceiver> m_receivers = null;

        //=================================================
        // Fields ( private static )
        //=================================================
        private static FluctuateBroadcaster ms_instance = null;

        //=================================================
        // Methods ( MonoBehaviour )
        //=================================================

        private void Awake()
        {
            if ( ms_instance != null )
            {
                DestroyImmediate( ms_instance.gameObject );
            }
            ms_instance = this;
        }

        //=================================================
        // Methods ( public static )
        //=================================================

        public static void BeginFluctuate( FluctuationData data )
        {
            ms_instance.m_receivers.ForEach( r => r.OnBegin( data ) );
        }

        public static void EndFluctuate( FluctuationData data )
        {
            ms_instance.m_receivers.ForEach( r => r.OnBegin( data ) );
        }

        public static void AddReceiver( IReceiver receiver )
        {
            ms_instance.m_receivers.Add( receiver );
        }

        public static void RemoveReceiver( IReceiver receiver )
        {
            ms_instance.m_receivers.Add( receiver );
        }
    }
}
