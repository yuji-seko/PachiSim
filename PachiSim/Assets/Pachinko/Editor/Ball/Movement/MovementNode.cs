using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Capacity = UnityEditor.Experimental.GraphView.Port.Capacity;

namespace Pachinko.Ball
{
    public abstract class MovementNode : Node
    {
        //=====================================================================
        // Fields( private )
        //=====================================================================
        private Movement m_movement = null;
        private Port m_upstream = null;
        private Port m_downstream = null;

        //=====================================================================
        // Properties( public )
        //=====================================================================
        public Movement Movement => m_movement;
        public Port Upstream => m_upstream;
        public Port Downstream => m_downstream;

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
                    m_upstream = CreatePort( Direction.Input, capacity );
                    m_upstream.portName = "Upstream";
                    inputContainer.Add( m_upstream );
                }
                Debug.Log( $"Upstreams: {movement.IsPossessableUpstreams}, {movement.PossessableUpstreamsCount}" );

                if ( movement.IsPossessableDownstreams )
                {
                    var capacity = movement.PossessableDownstreamsCount == 1 ? Capacity.Single : Capacity.Multi;
                    m_downstream = CreatePort( Direction.Output, capacity );
                    m_downstream.portName = "Downstream";
                    outputContainer.Add( m_downstream );
                }
                Debug.Log( $"Downstreams: {movement.IsPossessableDownstreams}, {movement.PossessableDownstreamsCount}" );
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

        public InjectionNode() : this( new Injection() ) { }
    }

    /// <summary>
    /// 分岐
    /// </summary>
    public class BounceNode : MovementNode<Bounce>
    {
        public BounceNode() : base( new Bounce() )
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
        public ChuckerNode() : base( new Chucker() )
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
        public OutNode() : base( new Out() )
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
        public WinNode() : base( new Win() )
        {
            name = nameof( WinNode );
            title = "Win";
        }
    }
}
