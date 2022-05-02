using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Pachinko.Ball
{
    /// <summary>
    /// 球の挙動を設定するビュー
    /// </summary>
    public class MovementGraphView : GraphView
    {
        //=====================================================================
        // Const( private static )
        //=====================================================================
        private readonly static string BackgroundUssPath = "Assets/Pachinko/Editor/Ball/Movement/UIElements/background.uss";

        //=====================================================================
        // Fields( private )
        //=====================================================================
        private MovementNodeFactory m_nodeFactory = new MovementNodeFactory();
        private InjectionNode m_rootNode = null;

        //=====================================================================
        // Properties( public )
        //=====================================================================
        public Movement RootMovement => m_rootNode.Movement;

        //=====================================================================
        // Methods( public )
        //=====================================================================

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MovementGraphView( MovementGraphEditorWindow editorWindow ) : base()
        {
            // 背景
            var background = AssetDatabase.LoadAssetAtPath( BackgroundUssPath, typeof( StyleSheet ) ) as StyleSheet;
            styleSheets.Add( background );
            Insert( 0, new GridBackground() );

            // ズーム可
            SetupZoom( ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale );

            // ドラッグ可
            this.AddManipulator( new SelectionDragger() );

            var menuWindowProvider = ScriptableObject.CreateInstance<SearchMenuWindowProvider>();
            menuWindowProvider.Initialize( this, editorWindow );

            // ノード作成可
            nodeCreationRequest += context =>
            {
                SearchWindow.Open( new SearchWindowContext( context.screenMousePosition ), menuWindowProvider );
            };

            graphViewChanged = new GraphViewChanged( OnChanged );
        }

        //=====================================================================
        // Methods( GraphView )
        //=====================================================================

        /// <summary>
        /// 接続可能なポートを取得する
        /// </summary>
        public override List<Port> GetCompatiblePorts( Port startAnchor, NodeAdapter nodeAdapter )
        {
            Debug.Log( $"GetCompatiblePorts : {startAnchor.portName}, {startAnchor.node.name}" );
            return ports.ToList();
        }

        /// <summary>
        /// ビューの変更時に呼ばれるコールバック
        /// </summary>
        private GraphViewChange OnChanged( GraphViewChange change )
        {
            Debug.Log( $"OnChanged:{change}" );

            change.edgesToCreate?.ForEach( edge =>
            {
                var upstream = edge.output?.node as MovementNode;
                var downstream = edge.input?.node as MovementNode;
                MovementConnector.Connect( upstream.Movement, downstream.Movement );
            } );

            change.elementsToRemove?.ForEach( element =>
            {
                if ( element is Edge edge )
                {
                    var upstream = edge.output?.node as MovementNode;
                    var downstream = edge.input?.node as MovementNode;
                    MovementConnector.Disconnect( upstream.Movement, downstream.Movement );
                }
            } );

            return change;
        }

        //=====================================================================
        // Methods( internal )
        //=====================================================================

        /// <summary>
        /// 初期化
        /// </summary>
        internal void Init( BallMovementAsset ball )
        {
            Reset();

            var injection = ball != null ? ball.Movement as Injection : new Injection();
            m_rootNode = AddNodeRecursive( injection ) as InjectionNode;
            m_rootNode.Focus();

            m_rootNode.RegisterCallback<GeometryChangedEvent>( RebuildFromInit );
        }

        /// <summary>
        /// リセット
        /// </summary>
        internal void Reset()
        {
            graphElements.ForEach( e => RemoveElement( e ) );
        }

        /// <summary>
        /// 作成可能なノードのリストを取得する
        /// </summary>
        internal IReadOnlyList<MovementType> GetCreatableNodeList()
        {
            return Enum.GetValues( typeof( MovementType ) )
                .ToArray<MovementType>()
                .Where( type => CanAddMovement( type ) )
                .ToArray();
        }

        /// <summary>
        /// ノード作成
        /// </summary>
        internal bool CreateNode( MovementType movementType, Vector2 position )
        {
            var node = m_nodeFactory.Create( movementType );
            node.SetPosition( new Rect( position, new Vector2( 100, 100 ) ) );
            AddElement( node );
            return true;
        }

        //=====================================================================
        // Methods( private )
        //=====================================================================

        private bool CanAddMovement( MovementType movementType )
        {
            switch ( movementType )
            {
                case MovementType.Injection:
                    // 打ち出しは一つのみ
                    return !nodes.Any( node => node is InjectionNode );
            }

            return true;
        }

        private MovementNode AddNodeRecursive( Movement movement )
        {
            var node = m_nodeFactory.Create( movement );
            AddElement( node );

            movement.Downstreams?.ForEach( ( downstream, index ) =>
            {
                var downstreamNode = AddNodeRecursive( downstream );
                var edge = node.Downstream.ConnectTo<Edge>( downstreamNode.Upstream );
                AddElement( edge );
            } );

            return node;
        }

        private void RebuildFromInit( GeometryChangedEvent e )
        {
            RebuildLayoutRecursive( m_rootNode );
            m_rootNode.UnregisterCallback<GeometryChangedEvent>( RebuildFromInit );
        }

        private void RebuildLayoutRecursive( MovementNode node, Vector2 position = new Vector2() )
        {
            var rect = node.GetPosition();
            node.SetPosition( new Rect( position.x, position.y, rect.width, rect.height ) );

            node.DownstreamNodes?.ForEach( ( downstream, index ) =>
            {
                var x = position.x + rect.width + 50;
                var y = position.y + index * 100;
                RebuildLayoutRecursive( downstream, new Vector2( x, y ) );
            } );
        }
    }
}

