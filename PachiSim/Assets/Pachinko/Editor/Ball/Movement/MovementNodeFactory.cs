namespace Pachinko.Ball
{
    public interface IMovementNodeFactory
    {
        MovementNode Create( MovementType movementType );
    }

    public class MovementNodeFactory : IMovementNodeFactory
    {
        public MovementNode Create( MovementType movementType )
        {
            switch ( movementType )
            {
                case MovementType.Injection:    return new InjectionNode();
                case MovementType.Bounce:       return new BounceNode();
                case MovementType.Chucker:      return new ChuckerNode();
                case MovementType.Out:          return new OutNode();
                case MovementType.Win:          return new WinNode();
                default:                        return null;
            }
        }
    }
}
