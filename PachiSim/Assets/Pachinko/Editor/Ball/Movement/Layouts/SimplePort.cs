using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using Capacity = UnityEditor.Experimental.GraphView.Port.Capacity;

namespace Pachinko.Ball
{
    /// <summary>
    /// ポート
    /// </summary>
    public class SimplePort : VisualElement
    {
        //=====================================================================
        // Fields ( private )
        //=====================================================================
        private readonly Port m_port = null;

        //=====================================================================
        // Properties ( public )
        //=====================================================================
        public Port         Port => m_port;
        public MovementNode Node => m_port.connections.FirstOrDefault()?.input?.node as MovementNode;

        //=====================================================================
        // Methods ( protected )
        //=====================================================================
        protected SimplePort( string portName )
        {
            m_port = Port.Create<Edge>( Orientation.Horizontal, Direction.Output, Capacity.Single, typeof( Port ) );
            m_port.portName = portName;
        }

        //=====================================================================
        // Methods ( public )
        //=====================================================================
        public SimplePort() : this( "Downstream" )
        {
            this.Add( m_port );
        }

        public T ConnectTo<T>( Port port ) where T : Edge, new()
        {
            return Port.ConnectTo<T>( port );
        }
    }
}
