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
                case MovementType.OutPocket: return new OutPocket();
                case MovementType.WinPocket: return new WinPocket();
                default: return null;
            }
        }
    }
}
