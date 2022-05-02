namespace Pachinko.Ball
{
    public interface IMovementFactory
    {
        Movement Create( MovementType movementType );
    }

    public sealed class MovementFactory : IMovementFactory
    {
        public Movement Create( MovementType movementType )
        {
            switch( movementType )
            {
                case MovementType.Injection: return new Injection();
                case MovementType.Bounce: return new Bounce();
                case MovementType.Chucker: return new Chucker();
                case MovementType.Out: return new Out();
                case MovementType.Win: return new Win();
                default: return null;
            }
        }
    }
}
