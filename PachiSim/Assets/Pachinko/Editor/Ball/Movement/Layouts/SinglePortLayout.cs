using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;

namespace Pachinko.Ball
{
    /// <summary>
    /// 単数ポートレイアウト
    /// </summary>
    public class SinglePortLayout : PortLayout
    {
        //=====================================================================
        // Fields ( private )
        //=====================================================================
        private SimplePort          m_port  = new SimplePort();
        private List<SimplePort>    m_ports = new List<SimplePort>();

        //=====================================================================
        // Properties ( public )
        //=====================================================================
        public override IReadOnlyList<Port>         Ports => m_ports.Select( p => p.Port ).ToArray();
        public override IReadOnlyList<MovementNode> Nodes => m_ports.Select( p => p.Node ).Where( p => p != null ).ToArray();

        //=====================================================================
        // Methods ( public )
        //=====================================================================

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SinglePortLayout()
        {
            Add( m_port );
            m_ports.Add( m_port );
        }
    }
}
