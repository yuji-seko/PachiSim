using UnityEngine;

namespace Pachinko.Ball
{
    public static class MovementConnector
    {
        public static void Connect( Movement upstream, Movement downstream )
        {
            upstream.AddDownstream( downstream );
            downstream.AddUpstream( upstream );
            Debug.Log( $"\tConnected : {upstream.MovementType} -> {downstream.MovementType}" );
        }

        public static void Disconnect( Movement upstream, Movement downstream )
        {
            upstream.RemoveDownstream( downstream );
            downstream.RemoveUpstream( upstream );
            Debug.Log( $"\tDisconnected : {upstream.MovementType} -> {downstream.MovementType}" );
        }
    }
}
