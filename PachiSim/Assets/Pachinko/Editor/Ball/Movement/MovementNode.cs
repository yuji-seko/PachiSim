using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Capacity = UnityEditor.Experimental.GraphView.Port.Capacity;
using System.Linq;
using System.Collections.Generic;

namespace Pachinko.Ball
{
    public abstract class MovementNode : Node
    {
        //=====================================================================
        // Fields( private )
        //=====================================================================
        private Movement m_movement = null;
        private Port m_upstreamPort = null;
        private Port m_downstreamPort = null;

        //=====================================================================
        // Properties( public )
        //=====================================================================
        public Movement Movement => m_movement;
        public Port Upstream => m_upstreamPort;
        public Port Downstream => m_downstreamPort;
        public IReadOnlyList<MovementNode> UpstreamNodes => m_upstreamPort?.connections?.Select( c => c.output?.node as MovementNode ).ToArray();
        public IReadOnlyList<MovementNode> DownstreamNodes => m_downstreamPort?.connections?.Select( c => c.input?.node as MovementNode ).ToArray();

        //=====================================================================
        // Methods( protected )
        //=====================================================================

        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected MovementNode( Movement movement )
        {
            m_movement = movement;

            if ( movement != null )
            {
                if ( movement.IsPossessableUpstreams )
                {
                    var capacity = movement.PossessableUpstreamsCount == 1 ? Capacity.Single : Capacity.Multi;
                    m_upstreamPort = CreatePort( Direction.Input, capacity );
                    m_upstreamPort.portName = "Upstream";
                    inputContainer.Add( m_upstreamPort );
                }
                //Debug.Log( $"Upstreams: {movement.IsPossessableUpstreams}, {movement.PossessableUpstreamsCount}" );

                if ( movement.IsPossessableDownstreams )
                {
                    var capacity = movement.PossessableDownstreamsCount == 1 ? Capacity.Single : Capacity.Multi;
                    m_downstreamPort = CreatePort( Direction.Output, capacity );
                    m_downstreamPort.portName = "Downstream";
                    outputContainer.Add( m_downstreamPort );
                }
                //Debug.Log( $"Downstreams: {movement.IsPossessableDownstreams}, {movement.PossessableDownstreamsCount}" );
            }
        }

        //=====================================================================
        // Methods( protected static )
        //=====================================================================

        /// <summary>
        /// ポート作成
        /// </summary>
        protected static Port CreatePort( Direction direction, Capacity capacity )
        {
            return Port.Create<Edge>( Orientation.Horizontal, direction, capacity, typeof( Port ) );
        }
    }

    /// <summary>
    /// 球の挙動
    /// </summary>
    public abstract class MovementNode<T> : MovementNode where T : Movement
    {
        new public T Movement => base.Movement as T;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected MovementNode( T movement ) : base( movement ) { }
    }

    /// <summary>
    /// 打ち出し
    /// </summary>
    public class InjectionNode : MovementNode<Injection>
    {
        public InjectionNode( Injection injection ) : base( injection )
        {
            name = nameof( InjectionNode );
            title = "Injection";
            capabilities -= Capabilities.Deletable;
        }
    }

    /// <summary>
    /// 分岐
    /// </summary>
    public class BounceNode : MovementNode<Bounce>
    {
        public BounceNode( Bounce bounce ) : base( bounce )
        {
            name = nameof( BounceNode );
            title = "Bounce";
        }
    }

    /// <summary>
    /// チャッカー
    /// </summary>
    public class ChuckerNode : MovementNode<Chucker>
    {
        public ChuckerNode( Chucker chucker ) : base( chucker )
        {
            name = nameof( ChuckerNode );
            title = "Chuker";
        }
    }

    /// <summary>
    /// アウト
    /// </summary>
    public class OutNode : MovementNode<Out>
    {
        public OutNode( Out _out ) : base( _out )
        {
            name = nameof( OutNode );
            title = "Out";
        }
    }

    /// <summary>
    /// 入賞
    /// </summary>
    public class WinNode : MovementNode<Win>
    {
        public WinNode( Win win ) : base( win )
        {
            name = nameof( WinNode );
            title = "Win";
        }
    }
}
