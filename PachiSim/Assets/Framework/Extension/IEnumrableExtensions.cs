namespace System.Collections.Generic
{
    public static class IEnumrableExtensions
    {
        public static IEnumerable<T> ForEach<T>( this IEnumerable<T> self, Action<T> action )
        {
            foreach( var element in self )
            {
                action.SafeCall( element );
            }
            return self;
        }

        public static IEnumerable<T> ForEach<T>( this IEnumerable<T> self, Action<T, int> action )
        {
            var index = 0;
            foreach ( var element in self )
            {
                action.SafeCall( element, index );
                index++;
            }
            return self;
        }
    }
}
