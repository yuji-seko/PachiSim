namespace System
{
    public static class ActionExtensions
    {
        public static void SafeCall( this Action self )
        {
            self?.Invoke();
        }

        public static void SafeCall<Arg1>( this Action<Arg1> self, Arg1 arg1 )
        {
            self?.Invoke( arg1 );
        }

        public static void SafeCall<Arg1, Arg2>( this Action<Arg1, Arg2> self, Arg1 arg1, Arg2 arg2 )
        {
            self?.Invoke( arg1, arg2 );
        }

        public static void SafeCall<Arg1, Arg2, Arg3>( this Action<Arg1, Arg2, Arg3> self, Arg1 arg1, Arg2 arg2, Arg3 arg3 )
        {
            self?.Invoke( arg1, arg2, arg3 );
        }

        public static void SafeCall<Arg1, Arg2, Arg3, Arg4>( this Action<Arg1, Arg2, Arg3, Arg4> self, Arg1 arg1, Arg2 arg2, Arg3 arg3, Arg4 arg4 )
        {
            self?.Invoke( arg1, arg2, arg3, arg4 );
        }
    }
}
