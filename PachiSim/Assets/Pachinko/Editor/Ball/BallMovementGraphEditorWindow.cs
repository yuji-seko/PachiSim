using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Pachinko
{
    /// <summary>
    /// 球の挙動を設定するエディタ
    /// </summary>
    public class BallMovementGraphEditorWindow : EditorWindow
    {
        [MenuItem( "Window/Pachinko/Ball Movement Graph", priority = 100 )]
        public static void Open()
        {
            GetWindow<BallMovementGraphEditorWindow>( "Ball Movement Graph" );
        }

        void OnEnable()
        {
            rootVisualElement.Add( new BallMovementGraphView()
            {
                style = { flexGrow = 1 },
            } );
        }
    }
}
