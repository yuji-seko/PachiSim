namespace Pachinko.Ball
{
    public interface IMovementNodeFactory
    {
        MovementNode Create( MovementType movementType );
    }

    public sealed class MovementNodeFactory : IMovementNodeFactory
    {
        private IMovementFactory m_movementFactory = new MovementFactory();

        public MovementNode Create( Movement movement )
        {
            switch ( movement.MovementType )
            {
                case MovementType.Injection: return new InjectionNode( movement as Injection );
                case MovementType.Bounce: return new BounceNode( movement as Bounce );
                case MovementType.Chucker: return new ChuckerNode( movement as Chucker );
                case MovementType.Out: return new OutNode( movement as Out );
                case MovementType.Win: return new WinNode( movement as Win );
                default: return null;
            }
        }

        public MovementNode Create( MovementType movementType )
        {
            return Create( m_movementFactory.Create( movementType ) );
        }
    }
}
