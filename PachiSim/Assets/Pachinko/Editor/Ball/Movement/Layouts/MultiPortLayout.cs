using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Pachinko.Ball
{
    /// <summary>
    /// 複数ポートレイアウト
    /// </summary>
    public class MultiPortLayout : PortLayout
    {
        //=====================================================================
        // Class ( private )
        //=====================================================================
        private class LayoutName
        {
            internal readonly static string PortLayout  = "PortLayout";
            internal readonly static string PlusButton  = "PlusButton";
            internal readonly static string MinusButton = "MinusButton";
        }

        //=====================================================================
        // Fields ( private const )
        //=====================================================================
        private readonly static string LayoutAssetPath = "Assets/Pachinko/Editor/Ball/Movement/UIElements/Downstreams.uxml";

        //=====================================================================
        // Fields ( private )
        //=====================================================================
        private VisualElement           m_baseLayout    = null;
        private List<IntegerFieldPort>  m_ports         = new List<IntegerFieldPort>();

        //=====================================================================
        // Properties ( public )
        //=====================================================================
        public override IReadOnlyList<Port>         Ports => m_ports.Select( p => p.Port ).ToArray();
        public override IReadOnlyList<MovementNode> Nodes => m_ports.Select( p => p.Node ).ToArray();

        //=====================================================================
        // Methods ( public )
        //=====================================================================

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MultiPortLayout( int portCount )
        {
            var visualTree = AssetDatabase.LoadAssetAtPath( LayoutAssetPath, typeof( VisualTreeAsset ) ) as VisualTreeAsset;
            visualTree.CloneTree( this );

            m_baseLayout = this.First( LayoutName.PortLayout );

            for ( int i = 0; i < portCount; i++ ) AddDownstreamPort();

            this.AddButtonEvent( LayoutName.PlusButton, () => AddDownstreamPort() );
            this.AddButtonEvent( LayoutName.MinusButton, RemoveDownstreamPort );
            this.SetEnabledButton( LayoutName.MinusButton, false );
        }

        //=====================================================================
        // Methods ( private )
        //=====================================================================

        private IntegerFieldPort AddDownstreamPort()
        {
            var port = new IntegerFieldPort();
            m_baseLayout.Add( port );

            m_ports.Add( port );

            this.SetEnabledButton( LayoutName.MinusButton, m_ports.Any() );

            return port;
        }

        private void RemoveDownstreamPort()
        {
            var port = m_ports.Last();
            m_baseLayout.Remove( port );
            m_ports.Remove( port );

            this.SetEnabledButton( LayoutName.MinusButton, m_ports.Any() );
        }
    }
}
