using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Pachinko.Ball
{
    /// <summary>
    /// 球の挙動を設定するエディタ
    /// </summary>
    public class MovementGraphEditorWindow : EditorWindow
    {
        //=====================================================================
        // Const( private static )
        //=====================================================================
        private readonly static string WindowName = "Movement Graph";
        private readonly static string LayoutAssetPath = "Assets/Pachinko/Editor/Ball/Movement/UIElements/MovementGraph.uxml";

        //=====================================================================
        // Fields( private )
        //=====================================================================
        private MovementGraphView m_view = null;
        private BallMovementAsset m_ball = null;
        private TextField m_assetNameText = null;

        //=====================================================================
        // Properties( public )
        //=====================================================================
        public MovementGraphView View => m_view;

        //=====================================================================
        // Methods( public static )
        //=====================================================================

        /// <summary>
        /// ツールバーから開く
        /// </summary>
        [MenuItem( "Window/Pachinko/Ball/Movement Graph", priority = 100 )]
        public static void Open()
        {
            var window = GetWindow<MovementGraphEditorWindow>( WindowName );
            window.SetAsset( null );
        }

        /// <summary>
        /// アセットファイルをダブルクリックで開く
        /// </summary>
        [OnOpenAsset( 0 )]
        public static bool OnOpenAsset( int instanceID, int line )
        {
            var asset = EditorUtility.InstanceIDToObject( instanceID ) as BallMovementAsset;

            if ( asset == null ) return false;

            var window = GetWindow<MovementGraphEditorWindow>( WindowName );
            window.SetAsset( asset );

            return true;
        }

        //=====================================================================
        // Methods( MonoBehaviour )
        //=====================================================================

        void OnEnable()
        {
            Debug.Log( "MovementGraphEditorWindow.OnEnable" );

            var visualTree = AssetDatabase.LoadAssetAtPath( LayoutAssetPath, typeof( VisualTreeAsset ) ) as VisualTreeAsset;
            visualTree.CloneTree( rootVisualElement );

            // アセット名
            m_assetNameText = rootVisualElement.Query<TextField>( "FileName" ).First();

            // ボタン
            SetButtonEvent( "SaveButton", Save );
            SetButtonEvent( "ResetButton", () => SetAsset( null ) );

            // ノード作成ビュー
            m_view = new MovementGraphView( this )
            {
                style = { flexGrow = 1 },
            };
            m_view.Init( null );
            rootVisualElement.Add( m_view );
        }

        //=====================================================================
        // Methods( private )
        //=====================================================================

        /// <summary>
        /// アセットの設定
        /// </summary>
        private void SetAsset( BallMovementAsset ball )
        {
            m_ball = ball;

            m_assetNameText.SetValueWithoutNotify( ball != null ? ball.Name : string.Empty );

            m_view.Init( ball );
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            if ( m_ball == null )
            {
                return;
            }

            m_ball.Name = m_assetNameText.text;
            m_ball.SetMovement( m_view.RootMovement );

            EditorUtility.SetDirty( m_ball );
            AssetDatabase.SaveAssets();

            Debug.Log( $"{m_ball.Name} is saved" );
        }

        /// <summary>
        /// ボタンイベントの設定
        /// </summary>
        private void SetButtonEvent( string buttonName, Action onClick )
        {
            var button = rootVisualElement.Query<Button>( buttonName ).First();
            if ( button != null )
            {
                button.clicked += onClick;
            }
        }
    }
}
