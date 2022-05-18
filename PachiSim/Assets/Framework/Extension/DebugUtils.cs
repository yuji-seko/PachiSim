using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pachinko
{
    public static class DebugUtils
    {
        public static string Yellow = "yellow";

        public static void LogColor( string color, string log, Object context ) => Debug.Log( $"<color={color}>{log}</color>", context );
        public static void LogColor( string color, string log ) => LogColor( color, log, null );
        public static void LogWhite( string log ) => LogColor( "white", log );
        public static void LogYellow( string log ) => LogColor( "yellow", log );
        public static void LogRed( string log ) => LogColor( "red", log );
        public static void LogBlue( string log ) => LogColor( "blue", log );
        public static void LogGreen( string log ) => LogColor( "green", log );
    }
}
