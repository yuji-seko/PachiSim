using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Capacity = UnityEditor.Experimental.GraphView.Port.Capacity;
using Pachinko;

namespace Pachinko.Ball
{
    public abstract class MovementNode : Node
    {
        //=====================================================================
        // Fields( private )
        //=====================================================================
        private Movement m_movement = null;
        private Port m_upstreamPort = null;
        private PortLayout m_downstreamPortLayout = null;

        //=====================================================================
        // Properties( public )
        //=====================================================================
        public Movement Movement => m_movement;
        public Port Upstream => m_upstreamPort;
        public IReadOnlyList<MovementNode> UpstreamNodes => m_upstreamPort?.connections?.Select( c => c.output?.node as MovementNode ).ToArray();
        public IReadOnlyList<MovementNode> DownstreamNodes => m_downstreamPortLayout?.Nodes;
        public IReadOnlyList<Port> DownstreamPorts => m_downstreamPortLayout?.Ports;

        //=====================================================================
        // Methods( protected )
        //=====================================================================

        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected MovementNode( Movement movement )
        {
            DebugUtils.LogYellow( $"MovementNode : {movement.MovementType}" );
            m_movement = movement;

            if ( movement != null )
            {
                BuildUpstreamLayout();
                BuildDownstreamLayout();
            }
        }

        //=====================================================================
        // Methods( protected static )
        //=====================================================================

        /// <summary>
        /// ポート作成
        /// </summary>
        private static Port CreatePort( Direction direction, Capacity capacity )
        {
            return Port.Create<Edge>( Orientation.Horizontal, direction, capacity, typeof( Port ) );
        }

        /// <summary>
        /// 上流ポートレイアウトの構築
        /// </summary>
        private void BuildUpstreamLayout()
        {
            DebugUtils.LogWhite( $"\tBuildUpstreamLayout() : {m_movement.IsPossessableUpstreams}, {m_movement.PossessableUpstreamsCount}" );

            if ( !m_movement.IsPossessableUpstreams ) return;

            var capacity = m_movement.PossessableUpstreamsCount == 1 ? Capacity.Single : Capacity.Multi;
            m_upstreamPort = CreatePort( Direction.Input, capacity );
            m_upstreamPort.portName = "Upstream";
            inputContainer.Add( m_upstreamPort );
        }

        /// <summary>
        /// 下流ポートレイアウトの構築
        /// </summary>
        private void BuildDownstreamLayout()
        {
            DebugUtils.LogWhite( $"\tBuildDownstreamLayout() : {m_movement.IsPossessableDownstreams}, {m_movement.PossessableDownstreamsCount}, {m_movement.Downstreams.Count}" );

            if ( !m_movement.IsPossessableDownstreams ) return;

            if ( m_movement.PossessableDownstreamsCount == 1 )
            {
                m_downstreamPortLayout = new SinglePortLayout();
            }
            else
            {
                m_downstreamPortLayout = new MultiPortLayout( m_movement.Downstreams.Count );
            }

            outputContainer.Add( m_downstreamPortLayout );
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
            title = nameof( Injection );
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
            title = nameof( Bounce );
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
            title = nameof( Chucker );
        }
    }

    /// <summary>
    /// アウト
    /// </summary>
    public class OutPocketNode : MovementNode<OutPocket>
    {
        public OutPocketNode( OutPocket _out ) : base( _out )
        {
            name = nameof( OutPocketNode );
            title = nameof( OutPocket );
        }
    }

    /// <summary>
    /// 入賞
    /// </summary>
    public class WinPocketNode : MovementNode<WinPocket>
    {
        public WinPocketNode( WinPocket win ) : base( win )
        {
            name = nameof( WinPocketNode );
            title = nameof( WinPocket );
        }
    }
}
