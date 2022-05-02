namespace System.Collections.Generic
{
    public static class IEnumrableExtensions
    {
        /// <summary>
        /// Listに変換
        /// </summary>
        public static List<T> ToList<T>( this IEnumerable self )
        {
            var list = new List<T>();
            foreach ( var value in self )
            {
                list.Add( ( T )value );
            }
            return list;
        }

        /// <summary>
        /// 配列に変換
        /// </summary>
        public static T[] ToArray<T>( this IEnumerable self )
        {
            return self.ToList<T>().ToArray();
        }

        /// <summary>
        /// foreach文処理
        /// </summary>
        public static IEnumerable<T> ForEach<T>( this IEnumerable<T> self, Action<T> action )
        {
            foreach( var element in self )
            {
                action.SafeCall( element );
            }
            return self;
        }

        /// <summary>
        /// foreach文処理
        /// </summary>
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
