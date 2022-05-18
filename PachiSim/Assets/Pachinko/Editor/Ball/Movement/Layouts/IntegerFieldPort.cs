using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Pachinko.Ball
{
    /// <summary>
    /// 数値入力付きのポート
    /// </summary>
    public class IntegerFieldPort : SimplePort
    {
        //=====================================================================
        // Class ( private )
        //=====================================================================
        private class LayoutName
        {
            internal readonly static string BaseLayout      = "BaseLayout";
            internal readonly static string InputFieldBase  = "InputFieldBase";
        }

        //=====================================================================
        // Fields ( private const )
        //=====================================================================
        private const string UxmlPath = "Assets/Pachinko/Editor/Ball/Movement/UIElements/IntegerFieldPort.uxml";

        //=====================================================================
        // Fields ( private )
        //=====================================================================
        private IntegerField m_ratio = null;

        //=====================================================================
        // Methods ( public )
        //=====================================================================

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public IntegerFieldPort() : base( string.Empty )
        {
            var visualTree = AssetDatabase.LoadAssetAtPath( UxmlPath, typeof( VisualTreeAsset ) ) as VisualTreeAsset;
            visualTree.CloneTree( this );

            var inputFiledBase = this.First( LayoutName.InputFieldBase );
            m_ratio = new IntegerField();
            inputFiledBase.Add( m_ratio );

            var layout = this.First( LayoutName.BaseLayout );
            layout.Insert( 0, Port );
        }
    }
}