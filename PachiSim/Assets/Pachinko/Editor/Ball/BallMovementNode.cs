using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

namespace Pachinko
{
    /// <summary>
    /// 球の挙動
    /// </summary>
    public abstract class BallMovementNode : Node
    {
        protected abstract int InputPortCount    { get; }
        protected abstract int OutputPortCount   { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected BallMovementNode()
        {
            if ( InputPortCount > 0 )
            {
                var capacity = InputPortCount == 1 ? Port.Capacity.Single : Port.Capacity.Multi;
                var port = Port.Create<Edge>( Orientation.Horizontal, Direction.Input, capacity, typeof( Port ) );
                inputContainer.Add( port );
            }

            if ( OutputPortCount > 0 )
            {
                var capacity = OutputPortCount == 1 ? Port.Capacity.Single : Port.Capacity.Multi;
                var port = Port.Create<Edge>( Orientation.Horizontal, Direction.Output, capacity, typeof( Port ) );
                outputContainer.Add( port );
            }
        }
    }

    /// <summary>
    /// 打ち出し
    /// </summary>
    public class BallInjectionNode : BallMovementNode
    {
        protected override int InputPortCount   => 0;
        protected override int OutputPortCount  => 1;

        public BallInjectionNode() : base()
        {
            title = "Injection";
        }
    }

    /// <summary>
    /// 分岐
    /// </summary>
    public class BallBranchNode : BallMovementNode
    {
        protected override int InputPortCount   => 1;
        protected override int OutputPortCount  => 2;

        public BallBranchNode() : base()
        {
            title = "Branch";
        }
    }
}
